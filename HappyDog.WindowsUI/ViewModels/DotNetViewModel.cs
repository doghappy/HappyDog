using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class DotNetViewModel : ArticleViewModel
    {
        protected override int CategoryId => 1;

        public async Task InitializeAsync()
        {
            await LoadArticleAsync();
        }
    }
}
