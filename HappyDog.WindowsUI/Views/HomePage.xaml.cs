using HappyDog.WindowsUI.Services;
using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class HomePage : Page, INotifyPropertyChanged
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private HomeViewModel viewModel;
        public HomeViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ViewModel)));
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationCacheMode = Configuration.ArticlePageCache;
            ViewModel = new HomeViewModel();
            await viewModel.InitializeAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (Configuration.ArticlePageCache == NavigationCacheMode.Disabled)
            {
                NavigationCacheMode = NavigationCacheMode.Enabled;
                Configuration.ArticlePageCache = NavigationCacheMode;
            }
        }

        private void PostArticle_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PostArticlePage));
        }
    }
}
