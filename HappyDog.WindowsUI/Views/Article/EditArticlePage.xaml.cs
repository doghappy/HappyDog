using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.ViewModels.Article;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class EditArticlePage : Page, INotifyPropertyChanged
    {
        public EditArticlePage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private EditArticleViewModel viewModel;
        public EditArticleViewModel ViewModel
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
            var article = e.Parameter as Models.Article;
            ViewModel = new EditArticleViewModel(article);
        }

        private async void Put_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.PutAsync();
        }
    }
}
