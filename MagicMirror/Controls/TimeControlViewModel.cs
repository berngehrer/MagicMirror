using MagicMirror.Contracts;
using MagicMirror.Model;
using MagicMirror.ViewModel;
using System;

namespace MagicMirror.Controls
{
    public class TimeControlViewModel : RefreshableViewModelBase<IAstroService>
    {
        public TimeControlViewModel()
        { }

        string _day;
        public string Day
        {
            get { return _day; }
            set { Set(ref _day, value); }
        }

        string _time;
        public string Time
        {
            get { return _time; }
            set { Set(ref _time, value); }
        }

        string _suntime;
        public string Suntime
        {
            get { return _suntime; }
            set { Set(ref _suntime, value); }
        }

        string _daylight;
        public string Daylight
        {
            get { return _daylight; }
            set { Set(ref _daylight, value); }
        }

        string _moonPercent;
        public string MoonPercent
        {
            get { return _moonPercent; }
            set { Set(ref _moonPercent, value); }
        }

        bool _showMoon;
        public bool ShowMoon
        {
            get { return _showMoon; }
            set { Set(ref _showMoon, value); }
        }

        AstroDynamic _sunDynamic;
        public AstroDynamic SunDynamic
        {
            get { return _sunDynamic; }
            set { Set(ref _sunDynamic, value); }
        }

        AstroDynamic _moonDynamic;
        public AstroDynamic MoonDynamic
        {
            get { return _moonDynamic; }
            set { Set(ref _moonDynamic, value); }
        }

        protected override void UpdateView()
        {
            Time = DateTime.Now.ToString("HH:mm");
            Day = DateTime.Now.ToString("dddd, dd. MMM");

            ShowMoon = !Service.Sun.IsShinning();
            MoonPercent = string.Format("{0:0.0}%", Service.Moon.Illumination).Replace(',', '.');
            MoonDynamic = Service.Moon.Dynamic;

            if (Service.Sun.IsShinning())
            {
                SunDynamic = AstroDynamic.Waning;
                Suntime = Service.Sun.Sunset.ToString("HH:mm");
                Daylight = $"{Service.Sun.Daylight.Hours}h {Service.Sun.Daylight.Minutes}m";
            }
            else
            {
                SunDynamic = AstroDynamic.Waxing;
                Suntime = Service.Sun.Sunrise.ToString("HH:mm");
                Daylight = "";
            }
        }

        protected override TimeSpan? ServiceInterval => TimeSpan.FromHours(1);
        protected override TimeSpan? RefreshInterval => TimeSpan.FromMinutes(1);
    }
}
