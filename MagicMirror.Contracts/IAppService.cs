using System;
using System.Threading.Tasks;

namespace MagicMirror.Contracts
{
    public interface IAppService : IDisposable
    {
        Task Reload();
    }

    public interface ISynronizedAppService : IAppService
    {
        event EventHandler Refreshed;

        void StartSynronize(TimeSpan reloadInterval);
        void StopSynronize();
    }
}
