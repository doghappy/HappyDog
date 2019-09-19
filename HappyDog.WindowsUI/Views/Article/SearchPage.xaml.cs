using HappyDog.WindowsUI.Requesters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class SearchPage : Page, INotifyPropertyChanged
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ArticleRequester _articleRequester;
        private string _queryText;
        private int _page;
        private bool _hasMore;

        private bool _noData;
        public bool NoData
        {
            get => _noData;
            set
            {
                if (_noData != value)
                {
                    _noData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoData)));
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _queryText = e.Parameter.ToString();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _page = 1;
            _hasMore = true;
            _articleRequester = new ArticleRequester();
            await SearchArticlesAsync();
        }

        private async Task SearchArticlesAsync()
        {
            if (_hasMore)
            {
                var result = await _articleRequester.SearchArticlesAsync(_queryText, _page);
                _hasMore = result.Data.TotalPages > _page;
                NoData = result.Data.TotalItems == 0;
                foreach (var item in result.Data.Data)
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
                    await SearchArticlesAsync();
                }
            }
        }
    }
}
