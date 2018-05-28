using HappyDog.WindowsUI.Services;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace HappyDog.WindowsUI.Views
{
    public partial class ArticleListBasePage : Page
    {
        public ArticleListBasePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationCacheMode = Configuration.ArticlePageCache;
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
    }
}
