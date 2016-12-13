using MagicMirror.Model;
using System.Collections.Generic;

namespace MagicMirror.Contracts
{
    public interface ICalendarService : ISynronizedAppService
    { 
        CalendarItem TodaysHoliday { get; }
        //IEnumerable<CalendarItem> GetHolidays(DateTime until);
        IEnumerable<CalendarItemGroup> GetPersonalCalendar(bool includeHolidays = false);
    }
}
