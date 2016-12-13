using Newtonsoft.Json;
using System;

namespace MagicMirror.Model.Json
{
    [JsonObject]
    public class AstroSunMainJson
    {
        [JsonProperty("astropositions")]
        public AstroSunInfoJson Astro { get; set; }
    }

    public class AstroSunInfoJson
    {
        [JsonProperty("informations")]
        public AstroSunObjectJson Information { get; set; }
    }

    public class AstroSunObjectJson
    {
        [JsonProperty("day")]
        public AstroSunJson Sun { get; set; }
    }

    public class AstroSunJson
    {
        [JsonProperty("sunrise")]
        public TimeSpan Sunrise { get; set; }
        [JsonProperty("zenith")]
        public TimeSpan Zenith { get; set; }
        [JsonProperty("sunset")]
        public TimeSpan Sunset { get; set; }
        [JsonProperty("daylight")]
        public AstroSunDurationJson Daytime { get; set; }
        [JsonProperty("night")]
        public AstroSunDurationJson Nighttime { get; set; }
    }

    public class AstroSunDurationJson
    {
        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }
    }

    

    [JsonObject]
    public class AstroMoonMainJson
    {
        [JsonProperty("age")]
        public double Age { get; set; }
        [JsonProperty("illumination")]
        public double Illumination { get; set; }
        [JsonProperty("stage")]
        public string Stage { get; set; }
        [JsonProperty("FM")]
        public AstroMoonTimeJson Fullmoon { get; set; }
        [JsonProperty("NNM")]
        public AstroMoonTimeJson Newmoon { get; set; }
    }

    public class AstroMoonTimeJson
    {
        [JsonProperty("UT")]
        public double Unixtime { get; set; }
    }
}
