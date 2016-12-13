using MagicMirror.Contracts;
using MagicMirror.Model;
using System;

namespace MagicMirror
{
    public class AppSettings : IAppSettings
    {
        
        #region Calendar

        public int CalenderPreviewDays
        {
            get
            {
                return 3;
            }
        }

        public string[] CalendarHeaders
        {
            get
            {
                return new[] { "Heute", "Morgen", "Übermorgen" };
            }
        }

        public Uri CalendarHolidayUri
        {
            get
            {
                return new Uri(@"https://calendar.google.com/calendar/ical/de.german%23holiday%40group.v.calendar.google.com/public/basic.ics");
            }
        }

        public Uri CalendarPersonalUri
        {
            get
            {
                return new Uri(@"");
            }
        }

        #endregion


        #region Weather

        public Uri WeatherHostUri
        {
            get
            {
                return new Uri("http://api.openweathermap.org");
            }
        }

        public Uri GetRelativeWeatherUri(WeatherType weather)
        {
            var template = "";
            
            string key = "";            
            switch (weather)
            {
                case WeatherType.ForecastHour:
                    key = "forecast";
                    break;
                case WeatherType.ForecaseDaily:
                    key = "forecast/daily";
                    break;
                default:
                    key = "weather";
                    break;
            }
            return new Uri(string.Format(template, key), UriKind.Relative);
        }

        public int WeatherForecastDisplayCount
        {
            get
            {
                return 3;
            }
        }

        #endregion


        #region Astro

        public Uri AstroSunUri
        {
            get
            {
                return new Uri("http://api.nginov.com/shared/ws/astro/?lat=48.35&lon=11.55&alt=465&mod=light&out=json");
            }
        }

        public Uri AstroMoonUri
        {
            get
            {
                return new Uri("http://api.burningsoul.in/moon");
            }
        }

        #endregion


        #region News

        public Uri[] NewsFeedUris
        {
            get
            {
                return new[] {
                    new Uri("http://www.spiegel.de/schlagzeilen/tops/index.rss"),
                    new Uri("http://www.n-tv.de/rss"),
                    new Uri("https://www.welt.de/feeds/latest.rss")
                };
            }
        }

        public int NewsFeedPerUriCount
        {
            get
            {
                return 10;
            }
        }

        #endregion

    }
}
