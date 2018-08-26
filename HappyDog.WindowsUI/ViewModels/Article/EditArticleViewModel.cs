using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Models.Results;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public class EditArticleViewModel : INotifyPropertyChanged
    {
        public EditArticleViewModel(int articleId)
        {
            ArticleId = articleId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Category> Categories => Configuration.Categories;

        public int ArticleId { get; }

        private Models.Article article;
        public Models.Article Article
        {
            get => article;
            set
            {
                article = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Article)));
            }
        }

        public async Task InitializeAsync()
        {
        }

        public async Task<HttpBaseResult> PutAsync()
        {
            //Article.CategoryId = Article.Category.Id;
            //return await articleService.PutAsync(Article);
            return null;
        }
    }
}
