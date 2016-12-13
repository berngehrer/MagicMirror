using System;
using System.Threading.Tasks;
using MagicMirror.Contracts;
using Windows.System.Threading;
using MagicMirror.Web;
using Newtonsoft.Json;

namespace MagicMirror.Data
{
    public abstract class RepositoryBase : ISynronizedAppService
    {
        HttpClientProxy _proxy;
        ThreadPoolTimer _timer;

        public event EventHandler Refreshed;

        protected RepositoryBase(IAppSettings settings)
            : this(settings, null)
        {
        }

        protected RepositoryBase(IAppSettings settings, Uri hostAddress)
        {
            Settings = settings;
            if (hostAddress == null) {
                _proxy = new HttpClientProxy();
            }
            else {
                _proxy = new HttpClientProxy(hostAddress);
            }
            Task.Run(Reload).Wait();
        }

        protected IAppSettings Settings { get; }

        public abstract Task Reload();


        public void StartSynronize(TimeSpan reloadInterval)
        {
            if (_timer == null) {
                _timer = ThreadPoolTimer.CreatePeriodicTimer(async _ => await Reload(), reloadInterval);
            }
        }

        public void StopSynronize()
        {
            _timer?.Cancel();
        }

        protected void FireRefreshed()
        {
            Refreshed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Dispose()
        {
            StopSynronize();
            _proxy.Dispose();
        }


        protected async Task<string> GetStringFromUrl(Uri url)
        {
            try {
                return await _proxy.ReadAsString(url);
            }
            catch { return null; }
        }

        protected async Task<T> GetFromUrl<T>(Uri url) where T : class, new()
        {
            var json = await GetStringFromUrl(url);
            if (!string.IsNullOrWhiteSpace(json)) {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }
    }
}
