using Windows.Storage;
using Windows.UI.Xaml;

namespace HappyDog.WindowsUI.ViewModels.Abstract
{
    public abstract class ArticleAuthViewModel : ArticleViewModel, IAuthentication
    {
        public ArticleAuthViewModel()
        {
            IsLogged = ApplicationData.Current.LocalSettings.Values.ContainsKey("AuthCookie")
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility IsLogged { get; }
    }
}
