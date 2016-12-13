using MagicMirror.Model;
using System;

namespace MagicMirror.Contracts
{
    public interface IAppSettings
    {
        // Calendar
        Uri CalendarHolidayUri { get; }
        Uri CalendarPersonalUri { get; }
        int CalenderPreviewDays { get; }
        string[] CalendarHeaders { get; }

        // Weather
        Uri WeatherHostUri { get; }
        Uri GetRelativeWeatherUri(WeatherType weather);
        int WeatherForecastDisplayCount { get; }

        // Astro
        Uri AstroSunUri { get; }
        Uri AstroMoonUri { get; }

        // News
        Uri[] NewsFeedUris { get; }
        int NewsFeedPerUriCount { get; }
    }
}
