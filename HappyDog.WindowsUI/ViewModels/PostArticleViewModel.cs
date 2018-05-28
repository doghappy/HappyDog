using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Models.Results;
using HappyDog.WindowsUI.Services;
using System.Collections.ObjectModel;
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
            Category = Categories.FirstOrDefault();
            articleService = new ArticleService();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        readonly ArticleService articleService;

        public ObservableCollection<Category> Categories => Configuration.Categories;

        private Category category;
        public Category Category
        {
            get => category;
            set
            {
                category = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category)));
            }
        }


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
