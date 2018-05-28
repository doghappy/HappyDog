using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels.Abstract
{
    public abstract class ArticleViewModel : INotifyPropertyChanged
    {
        readonly ArticleService articleService;

        public ArticleViewModel()
        {
            articleService = new ArticleService((int)Category);
            Articles = new ObservableCollection<Article>();
            HasMoreArticles = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

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

        protected virtual Enums.Category Category { get; }

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

        protected async Task<Article> LoadArticleAsync(int id)
        {
            return await articleService.GetArticleAsync(id);
        }
    }
}
