using System;

namespace MagicMirror.Model
{
    public static class WeatherExtensions
    {
        public static float GetFeelTemperature(this Weather w)
        {
            // New Wind Chill Factor (WCT)
            // https://en.wikipedia.org/wiki/Wind_chill
            if (w.Temperature < 4.4f && w.WindSpeed > 2.3f)
            {
                var windFactor = (float)Math.Pow(w.WindSpeed * 3.6, 0.16);
                return 13.12f + 0.6215f * w.Temperature - 11.37f * windFactor + 0.3965f * w.Temperature * windFactor;
            }
            // Humidex
            // https://de.wikipedia.org/wiki/Hitzeindex
            if (w.Temperature > 26.7f && w.Humidity > 40.0f)
            {
                double c1 = -8.784695d;
                double c2 = 1.61139411d;
                double c3 = 2.338549d;
                double c4 = -0.14611605d;
                double c5 = -1.2308094d * Math.Pow(10, -2);
                double c6 = -1.6424828d * Math.Pow(10, -2);
                double c7 = 2.211732d * Math.Pow(10, -3);
                double c8 = 7.2546d * Math.Pow(10, -4);
                double c9 = -3.582d * Math.Pow(10, -6);

                var tempPow = Math.Pow(w.Temperature, 2);
                var humPow = Math.Pow(w.Humidity, 2);

                return (float)(c1 +
                               c2 * w.Temperature +
                               c3 * w.Humidity +
                               c4 * w.Temperature * w.Humidity +
                               c5 * tempPow +
                               c6 * humPow +
                               c7 * tempPow * w.Humidity +
                               c8 * w.Temperature * humPow +
                               c9 * tempPow * humPow);
            }
            return w.Temperature;
        }

        public static int GetWindBeaufort(this Weather w)
        {
            // https://de.wikipedia.org/wiki/Beaufortskala
            float[] breakpoints = { 0.3f, 1.5f, 3.3f, 5.5f, 8.0f, 10.8f, 13.9f, 17.2f, 20.7f, 24.5f, 28.4f, 32.6f };
            for (int index = 0; index < breakpoints.Length; index++)
            {
                if (w.WindSpeed < breakpoints[index]) return index;
            }
            return breakpoints.Length;
        }

        public static string GetWindDegreeText(this Weather w)
        {
            // http://climate.umn.edu/snow_fence/components/winddirectionanddegreeswithouttable3.htm
            float[] breakpoints = { 11.25f, 33.75f, 56.25f, 78.75f, 101.25f, 123.75f, 146.25f, 168.75f, 191.25f, 213.75f, 236.25f, 258.75f, 281.25f, 303.75f, 326.25f, 348.75f };
            string[] directions = { "N", "NNO", "NO", "ONO", "O", "OSO", "SO", "SSO", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            for (int index = 0; index < breakpoints.Length; index++)
            {
                if (w.WindDegree < breakpoints[index]) return directions[index];
            }
            return directions[0];
        }

        public static WeatherState GetState(this Weather w)
        {
            // https://openweathermap.org/weather-conditions
            switch (w.Id)
            {
                case 200:   // thunderstorm with light rain
                case 210:   // light thunderstorm
                case 230:   // thunderstorm with light drizzle
                case 231:   // thunderstorm with drizzle
                    return WeatherState.ScatteredThunder;
                case 201:   // thunderstorm with rain
                case 202:   // thunderstorm with heavy rain
                case 211:   // thunderstorm
                case 212:   // heavy thunderstorm
                case 221:   // ragged thunderstorm
                case 232:   // thunderstorm with heavy drizzle
                    return WeatherState.Thunder;
                case 300:   //  light intensity drizzle
                case 310:   //  light intensity drizzle rain
                    return WeatherState.LightDrizzle;
                case 301:   //  drizzle
                case 302:   //  heavy intensity drizzle                
                case 311:   //  drizzle rain
                case 312:   //  heavy intensity drizzle rain
                case 313:   //  shower rain and drizzle
                case 314:   //  heavy shower rain and drizzle
                case 321:   //  shower drizzle
                    return WeatherState.Drizzle;
                case 500:   //  light rain
                case 501:   //  moderate rain
                case 520:   //  light intensity shower rain
                    return WeatherState.LightRain;
                case 502:   //  heavy intensity rain
                case 503:   //  very heavy rain
                case 504:   //  extreme rain                
                case 521:   //  shower rain
                case 522:   //  heavy intensity shower rain
                case 531:   //  ragged shower rain
                    return WeatherState.Rain;
                case 511:   //  freezing rain
                    return WeatherState.FrezzingRain;
                case 600:   // light snow
                case 611:   // sleet
                case 612:   // shower sleet
                case 620:   // light shower snow
                    return WeatherState.LightSnow;
                case 601:   // snow
                case 602:   // heavy snow
                case 621:   // shower snow
                case 622:   // heavy shower snow
                    return WeatherState.Snow;
                case 615:   // light rain and snow
                case 616:   // rain and snow
                    return WeatherState.SnowRain;
                case 701:   // mist
                case 711:   // smoke
                case 721:   // haze
                case 731:   // sand, dust whirls
                    return WeatherState.LightDust;
                case 741:   // fog
                case 751:   // sand
                case 761:   // dust
                case 762:   // volcanic ash
                case 771:   // squalls
                case 781:   // tornado
                    return WeatherState.Dust;
                case 801:   // few clouds  
                    return WeatherState.LightClounds;
                case 802:   // scattered clouds    
                case 803:   // broken clouds  
                    return WeatherState.MostlyClouds;
                case 804:   // overcast clouds  
                    return WeatherState.Clouds;
                case 800:   // clear sky
                    return WeatherState.ClearSky;
                default:    //  Default unknown
                    return WeatherState.Unknown;
            }
        }
    }
}
