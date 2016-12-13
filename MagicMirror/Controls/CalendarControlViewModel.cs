using MagicMirror.Contracts;
using MagicMirror.Model;
using MagicMirror.ViewModel;
using System;
using System.Collections.ObjectModel;

namespace MagicMirror.Controls
{
    public class CalendarControlViewModel : RefreshableViewModelBase<ICalendarService>
    {
        public CalendarControlViewModel()
        { }

        public ObservableCollection<CalendarItemGroup> CalendarEntries { get; private set; } = new ObservableCollection<CalendarItemGroup>();

        protected override void UpdateView()
        {
            var entries = Service.GetPersonalCalendar(includeHolidays: true);
            CalendarEntries = new ObservableCollection<CalendarItemGroup>(entries);
        }

        protected override TimeSpan? ServiceInterval => TimeSpan.FromMinutes(30);
    }
}
