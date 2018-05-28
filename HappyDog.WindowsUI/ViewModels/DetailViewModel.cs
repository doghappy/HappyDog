using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.ViewModels.Abstract;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class DetailViewModel : ArticleAuthViewModel
    {
        public DetailViewModel(int articleId)
        {
            this.articleId = articleId;
        }

        readonly int articleId;

        private Article article;
        public Article Article
        {
            get => article;
            set
            {
                article = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Article)));
            }
        }

        public async override Task InitializeAsync()
        {
            IsLoading = true;
            Article = await LoadArticleAsync(articleId);
            IsLoading = false;
        }
    }
}
