using MagicMirror.Model;

namespace MagicMirror.Contracts
{
    public interface IWeatherService : ISynronizedAppService
    {
        Weather Current { get; }
        Weather[] HourlyForecasts { get; }
        Weather[] DailyForecasts { get; }
    }
}
