using System;
using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HappyDog.WindowsUI.Services;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class PostArticlePage : Page, INotifyPropertyChanged
    {
        public PostArticlePage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private PostArticleViewModel viewModel;
        public PostArticleViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ViewModel)));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new PostArticleViewModel();
        }

        private async void Post_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var result = await ViewModel.PostAsync();
            if (result.Code == CodeResult.OK)
            {
                Configuration.ArticlePageCache = NavigationCacheMode.Disabled;
                Frame.Navigate(typeof(DetailPage), result.Data);
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "提示",
                    Content = result.Message,
                    PrimaryButtonText = "确定"
                };
                await dialog.ShowAsync();
            }
        }
    }
}
