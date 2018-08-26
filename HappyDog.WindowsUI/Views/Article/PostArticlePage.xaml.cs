using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views.Article
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

        private async void Post_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.PostAsync();
        }
    }
}
