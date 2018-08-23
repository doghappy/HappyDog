using HappyDog.WindowsUI.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class DetailViewModel:INotifyPropertyChanged
    {
        public DetailViewModel(int articleId)
        {
            this.articleId = articleId;
        }

        readonly int articleId;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public bool IsLogged => true;
        public bool IsLoading => false;

        public Task InitializeAsync()
        {
            //IsLoading = true;
            //Article = await LoadArticleAsync(articleId);
            //IsLoading = false;
            return null;
        }
    }
}
