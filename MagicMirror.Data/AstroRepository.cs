using MagicMirror.Contracts;
using MagicMirror.Model;
using MagicMirror.Model.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MagicMirror.Data
{
    public class AstroRepository : RepositoryBase, IAstroService
    {
        public AstroRepository(IAppSettings settings) 
            : base(settings)
        { }
        
        public async override Task Reload()
        {
            await ReloadSun();
            await ReloadMoon();
            FireRefreshed();
        }

        Sun _sun;
        public Sun Sun => _sun;

        public async Task ReloadSun()
        {
            var jsonSunContainer = await GetFromUrl<AstroSunMainJson>(Settings.AstroSunUri);
            var jsonSun = jsonSunContainer.Astro.Information.Sun;
            var newSun = new Sun
            {
                Night = jsonSun.Nighttime.Duration,
                Daylight = jsonSun.Daytime.Duration,
                Sunrise = DateHelper.TimeSpanToTodaysTime(jsonSun.Sunrise),
                Sunset = DateHelper.TimeSpanToTodaysTime(jsonSun.Sunset),
                Zenith = DateHelper.TimeSpanToTodaysTime(jsonSun.Zenith),

            };
            Interlocked.CompareExchange(ref _sun, newSun, _sun);
        }

        Moon _moon;
        public Moon Moon => _moon;

        public async Task ReloadMoon()
        {
            var jsonMoon = await GetFromUrl<AstroMoonMainJson>(Settings.AstroMoonUri);
            var newMoon = new Moon
            {
                Age = jsonMoon.Age,
                Illumination = jsonMoon.Illumination,
                Fullmoon = DateHelper.UnixTimeStampToDateTime(jsonMoon.Fullmoon.Unixtime),
                NewMoon = DateHelper.UnixTimeStampToDateTime(jsonMoon.Fullmoon.Unixtime),
                Dynamic = (AstroDynamic)Enum.Parse(typeof(AstroDynamic), jsonMoon.Stage, true)
            };
            Interlocked.CompareExchange(ref _moon, newMoon, _moon);
        }
    }
}
