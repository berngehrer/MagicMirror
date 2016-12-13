using MagicMirror.Model;

namespace MagicMirror.Contracts
{
    public interface IAstroService : ISynronizedAppService
    {
        Sun Sun { get; }
        Moon Moon { get; }
    }
}
