using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Models.Results;
using HappyDog.WindowsUI.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class EditArticleViewModel : INotifyPropertyChanged
    {
        public EditArticleViewModel(int articleId)
        {
            ArticleId = articleId;
            articleService = new ArticleService();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        readonly ArticleService articleService;

        public List<Category> Categories => Configuration.Categories;

        public int ArticleId { get; }

        private Article article;
        public Article Article
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
            Article = await articleService.GetArticleAsync(ArticleId);
        }

        public async Task<HttpBaseResult> PutAsync()
        {
            Article.CategoryId = Article.Category.Id;
            return await articleService.PutAsync(Article);
        }
    }
}
