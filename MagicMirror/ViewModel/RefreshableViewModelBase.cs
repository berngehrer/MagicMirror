using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using MagicMirror.Contracts;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml;

namespace MagicMirror.ViewModel
{
    public abstract class RefreshableViewModelBase<T> : ViewModelBase, IDisposable where T : ISynronizedAppService
    {
        DispatcherTimer _updateTimer;
        
        public RefreshableViewModelBase()
        {
            if (RefreshInterval.HasValue)
            {
                _updateTimer = new DispatcherTimer { Interval = RefreshInterval.Value };
                _updateTimer.Tick += (o, e) => UpdateView();
                _updateTimer.Start();
            }
            if (ServiceInterval.HasValue)
            {
                Service.StartSynronize(ServiceInterval.Value);
                Service.Refreshed += (o, e) => DispatcherHelper.CheckBeginInvokeOnUI(UpdateView);
            }
            UpdateView();
        }

        protected T Service { get; } = ServiceLocator.Current.GetInstance<T>();
        protected IAppSettings Settings { get; } = ServiceLocator.Current.GetInstance<IAppSettings>();
        
        protected abstract void UpdateView();
        protected virtual TimeSpan? ServiceInterval { get; }
        protected virtual TimeSpan? RefreshInterval { get; }

        public virtual void Dispose()
        {
            Service.StopSynronize();
            _updateTimer?.Stop();
        }
    }
}
