using System;
using Windows.UI.Xaml.Data;

namespace MagicMirror.Converter
{
    public class BeaufortTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int)
            {
                switch ((int)value)
                {
                    case 0: return "windstill";
                    case 1: return "leichter Zug";
                    case 2: return "leichte Brise";
                    case 3: return "schwache Brise";
                    case 4: return "mäßige Brise";
                    case 5: return "frische Brise";
                    case 6: return "starker Wind";
                    case 7: return "steifer Wind";
                    case 8: return "stürmischer Wind";
                    case 9: return "Sturm";
                    case 10: return "schwerer Sturm";
                    case 11: return "orkanartiger Sturm";
                    case 12: return "Orkan";
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
