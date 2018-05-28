using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.ViewModels.Abstract;

namespace HappyDog.WindowsUI.ViewModels
{
    public class ReadViewModel : ArticleAuthViewModel
    {
        protected override Category Category => Category.Read;
    }
}
