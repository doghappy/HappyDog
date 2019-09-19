using HappyDog.WindowsUI.Requesters;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.ComponentModel;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class DetailPage : Page, INotifyPropertyChanged
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _articleId;

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));
                }
            }
        }

        private Models.Article _article;
        public Models.Article Article
        {
            get => _article;
            set
            {
                if (_article != value)
                {
                    _article = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Article)));
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _articleId = (int)e.Parameter;
        }

        private async void MarkdownTextBlock_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                await Launcher.LaunchUriAsync(new Uri(e.Link));
            }
            catch { }
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            IsActive = true;
            var articleRequester = new ArticleRequester();
            Article = await articleRequester.GetArticleAsync(_articleId);
            IsActive = false;
        }
    }
}
