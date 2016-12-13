using MagicMirror.Model;

namespace MagicMirror.Contracts
{
    public interface INewsService : ISynronizedAppService
    {
        RssFeed GetNext();
    }
}
