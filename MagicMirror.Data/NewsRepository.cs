using MagicMirror.Concurrent;
using MagicMirror.Contracts;
using MagicMirror.Model;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace MagicMirror.Data
{
    public class NewsRepository : RepositoryBase, INewsService
    {
        int _feedIndex;
        int _uriIndex = 0;
        ConcurrentList<RssFeed> _feeds = new ConcurrentList<RssFeed>();

        public NewsRepository(IAppSettings settings) 
            : base(settings)
        { }

        public async override Task Reload()
        {
            var client = new SyndicationClient();
            var results = await client.RetrieveFeedAsync(Settings.NewsFeedUris[_uriIndex]);

            var feeds = results.Items.Take(Settings.NewsFeedPerUriCount).Select(x => new RssFeed
            {
                Channel = results.Title.Text,
                Title = WebUtility.HtmlDecode(x.Title.Text),
                Description = WebUtility.HtmlDecode(Regex.Replace(x.Summary.Text, "<.*?>", "").Trim())
            });

            _feedIndex = 0;
            SetNextUntil(Settings.NewsFeedUris.Length, ref _uriIndex);
            Interlocked.CompareExchange(ref _feeds, new ConcurrentList<RssFeed>(feeds.ToArray()), _feeds);

            FireRefreshed();
        }

        public RssFeed GetNext()
        {
            if (_feeds.Any())
            {
                var nextFeed = _feeds.ElementAt(_feedIndex);
                SetNextUntil(Settings.NewsFeedPerUriCount, ref _feedIndex);
                return nextFeed;
            }
            return null;
        }

        void SetNextUntil(int max, ref int value)
        {
            if (++value > (max - 1)) {
                value = 0;
            }
        }

        public override void Dispose()
        {
            _feeds.Clear();
            base.Dispose();
        }
    }
}
