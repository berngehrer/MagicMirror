using System;

namespace MagicMirror.Model
{
    public class Moon
    {
        public DateTime NewMoon { get; set; }
        public DateTime Fullmoon { get; set; }
        public AstroDynamic Dynamic { get; set; }
        public double Illumination { get; set; }
        public double Age { get; set; }
    }
}
