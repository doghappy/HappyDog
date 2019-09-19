using HappyDog.WindowsUI.Requesters;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            _page = 1;
            _hasMore = true;
            _articleRequester = new ArticleRequester();
        }

        private int _page;
        private bool _hasMore;
        private ArticleRequester _articleRequester;

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await GetArticlesAsync();
        }

        private async Task GetArticlesAsync()
        {
            if (_hasMore)
            {
                var result = await _articleRequester.GetArticlesAsync(_page);
                _hasMore = result.TotalPages > _page;
                _page++;
                foreach (var item in result.Data)
                {
                    ArticleList.Articles.Add(item);
                }
            }
        }

        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset <= 140)
            {
                if (!e.IsIntermediate && _hasMore)
                {
                    await GetArticlesAsync();
                }
            }
        }
    }
}
