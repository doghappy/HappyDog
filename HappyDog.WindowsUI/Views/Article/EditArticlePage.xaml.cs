using HappyDog.WindowsUI.ViewModels.Article;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int articleId = int.Parse(e.Parameter.ToString());
            ViewModel = new EditArticleViewModel(articleId);
            await ViewModel.InitializeAsync();
        }

        private async void Put_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //var result = await ViewModel.PutAsync();
            //if (result.Code == CodeResult.OK)
            //{
            //    Configuration.ArticlePageCache = NavigationCacheMode.Disabled;
            //    Frame.Navigate(typeof(DetailPage), ViewModel.ArticleId);
            //}
            //else
            //{
            //    var dialog = new ContentDialog
            //    {
            //        Title = "提示",
            //        Content = result.Message,
            //        PrimaryButtonText = "确定"
            //    };
            //    await dialog.ShowAsync();
            //}
        }
    }
}
