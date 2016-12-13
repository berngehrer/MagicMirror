using System;

namespace MagicMirror.Model
{
    public class Weather
    {
        public Weather(WeatherType type)
        {
            Type = type;
        }
        public WeatherType Type { get; }

        public int Id { get; set; }
        public string Description { get; set; }
        public float Temperature { get; set; }
        public float MinTemperature { get; set; }
        public float MaxTemperature { get; set; }
        public float Pressure { get; set; }
        public int Humidity { get; set; }
        public float WindSpeed { get; set; }
        public int WindDegree { get; set; }
        public int CloudPercent { get; set; }
        public DateTime MessureTime { get; set; }
    }
}
