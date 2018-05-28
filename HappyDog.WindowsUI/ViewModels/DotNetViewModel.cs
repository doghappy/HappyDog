using System.Threading.Tasks;
using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.ViewModels.Abstract;

namespace HappyDog.WindowsUI.ViewModels
{
    public class DotNetViewModel : ArticleAuthViewModel
    {
        protected override Category Category => Category.DotNet;
    }
}
