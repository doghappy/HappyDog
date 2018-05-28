using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Models.Results;
using HappyDog.WindowsUI.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class PostArticleViewModel : INotifyPropertyChanged
    {
        public PostArticleViewModel()
        {
            Article = new Article
            {
                Category = Categories.FirstOrDefault()
            };
            articleService = new ArticleService();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        readonly ArticleService articleService;

        public List<Category> Categories => Configuration.Categories;

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

        public async Task<HttpDataResult<int>> PostAsync()
        {
            Article.CategoryId = Article.Category.Id;
            return await articleService.PostAsync(Article);
        }
    }
}
