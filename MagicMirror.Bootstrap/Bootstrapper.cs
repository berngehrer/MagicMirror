using MagicMirror.Contracts;
using MagicMirror.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace MagicMirror.Bootstrap
{
    public static class Bootstrapper
    {
        static UnityContainer _container;
        internal static UnityContainer Container => _container;

        public static void Initialize(IAppSettings settings)
        {
            _container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(Container));

            Container.RegisterInstance(settings);
            RegisterContracts();
        }

        static void RegisterContracts()
        {
            Container.RegisterType<INewsService, NewsRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAstroService, AstroRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IWeatherService, WeatherRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICalendarService, CalendarRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
