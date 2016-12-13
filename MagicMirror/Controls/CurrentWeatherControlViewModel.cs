using MagicMirror.Contracts;
using MagicMirror.Model;
using MagicMirror.ViewModel;
using System;

namespace MagicMirror.Controls
{
    public class CurrentWeatherControlViewModel : RefreshableViewModelBase<IWeatherService>
    {
        public CurrentWeatherControlViewModel()
        { }

        WeatherDisplayContainer _weather;
        public WeatherDisplayContainer Weather
        {
            get { return _weather; }
            set { Set(ref _weather, value); }
        }

        protected override void UpdateView()
        {
            Weather = new WeatherDisplayContainer(Service.Current);
        }

        protected override TimeSpan? ServiceInterval => TimeSpan.FromMinutes(20);
    }


    public class WeatherDisplayContainer
    {
        public WeatherDisplayContainer(Weather w)
        {
            WindDirection = $"({w.GetWindDegreeText()})";
            Temperature = w.GetFeelTemperature();
            Beaufort = w.GetWindBeaufort();
            State = w.GetState();
            Description = w.Description;
            RealTemperature = w.Temperature;
        }

        public float Temperature { get; }
        public float RealTemperature { get; }
        public string Description { get; }
        public int Beaufort { get; }
        public string WindDirection { get; }
        public WeatherState State { get; }
        public bool ShowReal => Temperature != RealTemperature;
    }
}
