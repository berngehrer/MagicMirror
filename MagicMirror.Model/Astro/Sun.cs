using System;

namespace MagicMirror.Model
{
    public class Sun
    {
        public DateTime Zenith { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public TimeSpan Daylight { get; set; }
        public TimeSpan Night { get; set; }
        
        public bool IsShinning(DateTime dt)
        {
            return dt >= Sunrise && dt < Sunset;
        }

        public bool IsShinning() => IsShinning(DateTime.Now);
    }
}
