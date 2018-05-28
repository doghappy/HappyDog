using Windows.UI.Xaml;

namespace HappyDog.WindowsUI.ViewModels.Abstract
{
    public interface IAuthentication
    {
        Visibility IsLogged { get; }
    }
}
