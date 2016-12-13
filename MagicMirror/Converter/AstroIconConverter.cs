using MagicMirror.Model;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MagicMirror.Converter
{
    public class AstroIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isMoon = parameter?.ToString() == "Moon";
            if (value is AstroDynamic)
            {
                switch ((AstroDynamic)value)
                {
                    case AstroDynamic.Waxing:
                        return isMoon 
                                ? new BitmapImage(new Uri("ms-appx://MagicMirror/Assets/moonwaxing.png"))
                                : new BitmapImage(new Uri("ms-appx://MagicMirror/Assets/sunrise.png"));
                    case AstroDynamic.Waning:
                        return isMoon
                                ? new BitmapImage(new Uri("ms-appx://MagicMirror/Assets/moonwaning.png"))
                                : new BitmapImage(new Uri("ms-appx://MagicMirror/Assets/sunset.png"));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
