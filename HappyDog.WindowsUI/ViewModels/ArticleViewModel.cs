using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public abstract class ArticleViewModel : INotifyPropertyChanged
    {
        readonly ArticleService articleService;

        public ArticleViewModel()
        {
            articleService = new ArticleService(CategoryId);
            Articles = new ObservableCollection<Article>();
            HasMoreArticles = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Article> Articles { get; }

        private int pageNumber;
        public int PageNumber
        {
            get => pageNumber;
            set
            {
                pageNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageNumber)));
            }
        }

        private int totalPages;
        public int TotalPages
        {
            get => totalPages;
            set
            {
                totalPages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPages)));
            }
        }

        protected virtual int CategoryId { get; }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }

        public bool HasMoreArticles { get; private set; }
        public async Task LoadArticleAsync()
        {
            if (HasMoreArticles)
            {
                PageNumber++;
                var articles = await articleService.GetArticlesAsync(PageNumber);
                foreach (var item in articles.Data)
                {
                    Articles.Add(item);
                }
                TotalPages = articles.TotalPages;
                HasMoreArticles = PageNumber != TotalPages;
            }
        }

        public async virtual Task InitializeAsync()
        {
            IsLoading = true;
            await LoadArticleAsync();
            IsLoading = false;
        }
    }
}
