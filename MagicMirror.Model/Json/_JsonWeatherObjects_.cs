using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicMirror.Model.Json
{
    [JsonObject]
    public class WeatherDailyForecastJson
    {
        [JsonProperty("list")]
        public List<WeatherDailyContainerJson> Items { get; set; }
    }

    [JsonObject]
    public class WeatherHourForecastJson
    {
        [JsonProperty("list")]
        public List<WeatherCurrentMainJson> Items { get; set; }
    }
    
    [JsonObject]
    public class WeatherCurrentMainJson
    {
        [JsonProperty("weather")]
        public List<WeatherContainerJson> Main { get; set; }
        [JsonProperty("main")]
        public WeatherAirJson Air { get; set; }
        [JsonProperty("wind")]
        public WeatherWindJson Wind { get; set; }
        [JsonProperty("clouds")]
        public WeatherCloudJson Clouds { get; set; }
        [JsonProperty("dt")]
        public double Timestamp { get; set; }
    }

    public class WeatherDailyContainerJson
    {
        [JsonProperty("weather")]
        public List<WeatherContainerJson> Main { get; set; }
        [JsonProperty("temp")]
        public WeatherAirDailyJson Air { get; set; }
        [JsonProperty("pressure")]
        public float Pressure { get; set; }
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        [JsonProperty("speed")]
        public float WindSpeed { get; set; }
        [JsonProperty("deg")]
        public float WindDegree { get; set; }
        [JsonProperty("clouds")]
        public float Clouds { get; set; }
        [JsonProperty("dt")]
        public double Timestamp { get; set; }
    }

    public class WeatherAirJson
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }
        [JsonProperty("pressure")]
        public float Pressure { get; set; }
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        [JsonProperty("temp_min")]
        public float MinTemperature { get; set; }
        [JsonProperty("temp_max")]
        public float MaxTemperature { get; set; }
        [JsonProperty("temp_kf")]
        public float FeelTemperature { get; set; }
    }

    public class WeatherAirDailyJson
    {
        [JsonProperty("day")]
        public float Temperature { get; set; }
        [JsonProperty("min")]
        public float MinTemperature { get; set; }
        [JsonProperty("max")]
        public float MaxTemperature { get; set; }
    }

    public class WeatherContainerJson
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class WeatherWindJson
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public float Degree { get; set; }
    }

    public class WeatherCloudJson
    {
        [JsonProperty("all")]
        public double Value { get; set; }
    }
}
