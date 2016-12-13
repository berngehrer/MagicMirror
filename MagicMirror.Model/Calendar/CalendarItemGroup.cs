using System.Collections.ObjectModel;

namespace MagicMirror.Model
{
    public class CalendarItemGroup : ObservableCollection<CalendarItem>
    {
        public CalendarItemGroup(string header, params CalendarItem[] items)
            : base(items)
        {
            Header = header;
        }
        public string Header { get; }
        public new ObservableCollection<CalendarItem> Items => this;
    }
}
