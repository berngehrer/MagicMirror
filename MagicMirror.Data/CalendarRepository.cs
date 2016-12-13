using MagicMirror.Concurrent;
using MagicMirror.Contracts;
using MagicMirror.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MagicMirror.Data
{
    public class CalendarRepository : RepositoryBase, ICalendarService
    {
        ConcurrentList<CalendarItem> _holidayItems = new ConcurrentList<CalendarItem>();
        ConcurrentList<CalendarItem> _calendarItems = new ConcurrentList<CalendarItem>();
        
        public CalendarRepository(IAppSettings settings) 
            : base(settings)
        { }
        
        public override void Dispose()
        {
            _holidayItems.Clear();
            _calendarItems.Clear();
        }
        
        public async override Task Reload()
        {
            var holidays = await Request_iCal(Settings.CalendarHolidayUri); 
            var calendar = await Request_iCal(Settings.CalendarPersonalUri);

            var today = holidays?.FirstOrDefault(x => x.Start.IsToday());
            if (today != null) {
                today.Summary = today.Summary.Split('(')[0].Trim();
            }
            Interlocked.CompareExchange(ref _todaysHoliday, today, _todaysHoliday);

            Interlocked.CompareExchange(ref _holidayItems, new ConcurrentList<CalendarItem>(holidays), _holidayItems);
            Interlocked.CompareExchange(ref _calendarItems, new ConcurrentList<CalendarItem>(calendar), _calendarItems);

            FireRefreshed();
        }


        CalendarItem _todaysHoliday;
        public CalendarItem TodaysHoliday => _todaysHoliday;

        public IEnumerable<CalendarItemGroup> GetPersonalCalendar(bool includeHolidays = false)
        {
            for (int i = 0; i < Settings.CalenderPreviewDays; i++)
            {
                var day = DateTime.Now.AddDays(i);
                var dayEntries = GetActiveEntriesFromDay(_calendarItems, day);
                if (includeHolidays) {
                    var holidays = GetActiveEntriesFromDay(_holidayItems, day);
                    dayEntries = holidays.Concat(dayEntries);
                }
                yield return new CalendarItemGroup(Settings.CalendarHeaders[i], dayEntries.ToArray());
            }
        }

        /// <summary>
        /// Filter items from defined day which are not yet finished. Ordered by start time
        /// </summary>
        IEnumerable<CalendarItem> GetActiveEntriesFromDay(IEnumerable<CalendarItem> items, DateTime day)
        {
            return items.Where(x => x.Start.IsSameDay(day) && x.End > DateTime.Now).OrderBy(x => x.Start);
        }


        async Task<List<CalendarItem>> Request_iCal(Uri url)
        {
            CalendarItem currentItem = null;
            List<CalendarItem> items = new List<CalendarItem>();

            var content = await GetStringFromUrl(url);
            if (content == null) {
                return items;
            }
            var lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line == "BEGIN:VEVENT")
                {
                    currentItem = new CalendarItem();
                }
                else if (line == "END:VEVENT")
                {
                    items.Add(currentItem);
                }
                else if (currentItem != null)
                {
                    var parts = line.Split(':');
                    var value = parts.Length > 1 ? parts[1] : "";
                    var pair = new KeyValuePair<string, string>(parts[0], value);

                    if (pair.Key.StartsWith("UID"))
                    {
                        currentItem.Uid = pair.Value;
                    }
                    else if (pair.Key.StartsWith("SUMMARY"))
                    {
                        currentItem.Summary = pair.Value;
                    }
                    else if (pair.Key.StartsWith("LOCATION"))
                    {
                        currentItem.Location = pair.Value;
                    }
                    else if (pair.Key.StartsWith("DESCRIPTION"))
                    {
                        currentItem.Description = pair.Value;
                    }
                    else if (pair.Key.StartsWith("CREATED"))
                    {
                        currentItem.Created = DateHelper.ParseExactDateTime(pair.Value);
                    }
                    else if (pair.Key.StartsWith("DTSTART"))
                    {
                        currentItem.Start = DateHelper.ParseExactDateTime(pair.Value);
                    }
                    else if (pair.Key.StartsWith("DTEND"))
                    {
                        currentItem.End = DateHelper.ParseExactDateTime(pair.Value);
                    }
                }
            }
            return items; 
        }
    }
}
