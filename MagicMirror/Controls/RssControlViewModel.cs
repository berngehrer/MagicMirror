using MagicMirror.Contracts;
using MagicMirror.Model;
using MagicMirror.ViewModel;
using System;

namespace MagicMirror.Controls
{
    public class RssControlViewModel : RefreshableViewModelBase<INewsService>
    {
        public RssControlViewModel() 
        { }

        RssFeed _currentFeed;
        public RssFeed CurrentFeed
        {
            get { return _currentFeed; }
            set { Set(ref _currentFeed, value); }
        }

        protected override TimeSpan? ServiceInterval => TimeSpan.FromMinutes(15);
        protected override TimeSpan? RefreshInterval => TimeSpan.FromSeconds(30);

        protected override void UpdateView()
        {
            CurrentFeed = Service.GetNext();
        }
    }
}
