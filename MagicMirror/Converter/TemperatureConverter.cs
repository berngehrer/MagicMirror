using System;
using Windows.UI.Xaml.Data;

namespace MagicMirror.Converter
{
    public class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is float)
            {
                return string.Format("{0:0.0}°", value).Replace(',', '.');
            }
            return "---";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
