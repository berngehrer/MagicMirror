using MagicMirror.Model;
using System;
using Windows.UI.Xaml.Data;

namespace MagicMirror.Converter
{
    public class CalendarTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = value as CalendarItem;
            if (item != null)
            {
                if (!item.IsFullday()) {
                    return string.Format("{0:00}:{1:00}", item.Start.Hour, item.Start.Minute);
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
