using MagicMirror.Contracts;
using MagicMirror.Model;
using MagicMirror.Model.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MagicMirror.Data
{
    public class WeatherRepository : RepositoryBase, IWeatherService
    {
        public WeatherRepository(IAppSettings settings)
           : base(settings, settings.WeatherHostUri)
        {
        }
         
        Weather _current;
        public Weather Current => _current;

        Weather[] _hourlyForecasts;
        public Weather[] HourlyForecasts => _hourlyForecasts;

        Weather[] _dailyForecasts;
        public Weather[] DailyForecasts => _dailyForecasts;


        public async override Task Reload()
        {
            await ReloadCurrent();
            await ReloadHourly();
            await ReloadDaily();
            FireRefreshed();
        }

        async Task ReloadCurrent()
        {
            var currentJson = await GetJsonForWeather<WeatherCurrentMainJson>(WeatherType.Current);
            var currentNew = new Weather(WeatherType.Current);
            FillModelTypeA(currentJson, ref currentNew);
            Interlocked.CompareExchange(ref _current, currentNew, _current);
        }

        async Task ReloadHourly()
        {
            List<Weather> hourlyItems = new List<Weather>();
            var hourlyJson = await GetJsonForWeather<WeatherHourForecastJson>(WeatherType.ForecastHour);
            foreach (var item in hourlyJson.Items)
            {
                var hourlyNew = new Weather(WeatherType.ForecastHour);
                FillModelTypeA(item, ref hourlyNew);
                hourlyItems.Add(hourlyNew);
            }
            Interlocked.CompareExchange(ref _hourlyForecasts, hourlyItems.ToArray(), _hourlyForecasts);
        }

        async Task ReloadDaily()
        {
            List<Weather> dailyItems = new List<Weather>();
            var dailyJson = await GetJsonForWeather<WeatherDailyForecastJson>(WeatherType.ForecaseDaily);
            foreach (var item in dailyJson.Items)
            {
                var dailyNew = new Weather(WeatherType.ForecaseDaily);
                FillModelTypeB(item, ref dailyNew);
                dailyItems.Add(dailyNew);
            }
            Interlocked.CompareExchange(ref _dailyForecasts, dailyItems.ToArray(), _dailyForecasts);
        }


        async Task<T> GetJsonForWeather<T>(WeatherType type)
        {
            var url = Settings.GetRelativeWeatherUri(type);
            var json = await GetStringFromUrl(url);
            return JsonConvert.DeserializeObject<T>(json);
        }

        void FillModelTypeA(WeatherCurrentMainJson jsonObject, ref Weather weather)
        {
            weather.Id = jsonObject.Main[0].Id;
            weather.Description = jsonObject.Main[0].Description;
            weather.Temperature = jsonObject.Air.Temperature;
            weather.MaxTemperature = jsonObject.Air.MaxTemperature;
            weather.MinTemperature = jsonObject.Air.MinTemperature;
            weather.Pressure = jsonObject.Air.Pressure;
            weather.Humidity = (int)jsonObject.Air.Humidity;
            weather.CloudPercent = (int)jsonObject.Clouds.Value;
            weather.WindDegree = (int)jsonObject.Wind.Degree;
            weather.WindSpeed = jsonObject.Wind.Speed;
            weather.MessureTime = DateHelper.UnixTimeStampToDateTime(jsonObject.Timestamp);
        }

        void FillModelTypeB(WeatherDailyContainerJson jsonObject, ref Weather weather)
        {
            weather.Id = jsonObject.Main[0].Id;
            weather.Description = jsonObject.Main[0].Description;
            weather.Temperature = jsonObject.Air.Temperature;
            weather.MaxTemperature = jsonObject.Air.MaxTemperature;
            weather.MinTemperature = jsonObject.Air.MinTemperature;
            weather.Pressure = jsonObject.Pressure;
            weather.Humidity = (int)jsonObject.Humidity;
            weather.CloudPercent = (int)jsonObject.Clouds;
            weather.WindDegree = (int)jsonObject.WindDegree;
            weather.WindSpeed = jsonObject.WindSpeed;
            weather.MessureTime = DateHelper.UnixTimeStampToDateTime(jsonObject.Timestamp);
        }
    }
}
