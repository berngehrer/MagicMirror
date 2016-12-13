using MagicMirror.Contracts;
using MagicMirror.Model;
using MagicMirror.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MagicMirror.Controls
{
    public class ForecastWeatherControlViewModel : RefreshableViewModelBase<IWeatherService>
    {
        bool _isdailyView;

        public ForecastWeatherControlViewModel()
        { }

        public ObservableCollection<ForecastDisplayContainer> Forecasts { get; } = new ObservableCollection<ForecastDisplayContainer>();
        
        protected override void UpdateView()
        {
            var items = GetItemSource().Where(StartFilter).Take(Settings.WeatherForecastDisplayCount);

            Forecasts.Clear();
            foreach (var forecast in items) {
                Forecasts.Add(new ForecastDisplayContainer(forecast));
            }
        }

        IEnumerable<Weather> GetItemSource()
        {
            return ((_isdailyView = !_isdailyView))
                    ? Service.DailyForecasts
                    : Service.HourlyForecasts;
        }

        bool StartFilter(Weather w) => w.MessureTime > DateTime.Now.ToDayStart();

        protected override TimeSpan? ServiceInterval => TimeSpan.FromMinutes(20);
        protected override TimeSpan? RefreshInterval => TimeSpan.FromSeconds(20);
    }


    public class ForecastDisplayContainer
    {
        public ForecastDisplayContainer(Weather w)
        {
            Original = w;
            State = w.GetState();
            Temperature = string.Format("{0:0}°", w.Temperature);
            if (w.Type == WeatherType.ForecaseDaily) {
                Header = w.MessureTime.ToString("ddd");
            }
            else if (w.Type == WeatherType.ForecastHour) {
                Header = $"{w.MessureTime.Hour}h";
            }
        }

        public Weather Original { get; }
        public string Header { get; }
        public string Temperature { get; }
        public WeatherState State { get; }
    }
}
