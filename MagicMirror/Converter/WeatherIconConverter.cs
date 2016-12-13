using MagicMirror.Contracts;
using MagicMirror.Model;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MagicMirror.Converter
{
    public class WeatherIconConverter : IValueConverter
    {
        IAstroService Astro { get; } = ServiceLocator.Current.GetInstance<IAstroService>();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isDay = Astro.Sun.IsShinning();
            WeatherState state = WeatherState.Unknown;
            
            if (value is Weather)
            {
                var weather = value as Weather;
                state = weather.GetState();

                switch (weather.Type)
                {
                    case WeatherType.ForecastHour:
                        isDay = Astro.Sun.IsShinning(weather.MessureTime);
                        break;
                    case WeatherType.ForecaseDaily:
                        isDay = true;
                        break;
                }
            }
            else if (value is WeatherState)
            {
                state = (WeatherState)value;
            }
            return new BitmapImage(new Uri($"ms-appx://MagicMirror/Assets/Weather/{GetIcon(state, isDay)}.png"));
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }


        string GetIcon(WeatherState state, bool isDay)
        {
            switch (state)
            {
                case WeatherState.ClearSky:
                    return isDay ? "clear-day" : "clear-night";
                case WeatherState.ScatteredThunder:
                    return isDay ? "scattered-thunder-day" : "scattered-thunder-night";
                case WeatherState.Thunder:
                    return "thunder";
                case WeatherState.LightDrizzle:
                    return "light-dizzle";
                case WeatherState.Drizzle:
                    return "dizzle";
                case WeatherState.LightRain:
                    return isDay ? "light-rain-day" : "light-rain-night";
                case WeatherState.Rain:
                    return "rain";
                case WeatherState.FrezzingRain:
                    return "frezzing-rain";
                case WeatherState.LightSnow:
                    return "light-snow";
                case WeatherState.Snow:
                    return "snow";
                case WeatherState.SnowRain:
                    return "rain-snow";
                case WeatherState.LightDust:
                    return isDay ? "light-dust" : "dust";
                case WeatherState.Dust:
                    return "dust";
                case WeatherState.LightClounds:
                    return isDay ? "light-cloudy-day" : "light-cloudy-night";
                case WeatherState.MostlyClouds:
                    return isDay ? "mostly-cloudy-day" : "mostly-cloudy-night";
                case WeatherState.Clouds:
                    return "cloudy";
            }
            return "unknown";
        }
    }
}
