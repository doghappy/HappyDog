using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.ViewModels
{
    public abstract class ArticleViewModel : INotifyPropertyChanged
    {
        public ArticleViewModel()
        {
            Articles = new ObservableCollection<Article>();
            PageNumber = 1;
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

        protected async Task LoadArticleAsync()
        {
            IsLoading = true;
            var svc = new ArticleService();
            try
            {
                var articles = await svc.GetArticlesAsync(PageNumber, CategoryId);
                Articles.Clear();
                foreach (var item in articles.Data)
                {
                    Articles.Add(item);
                }
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    PrimaryButtonText = "确定",
                    Title = "错误",
                    Content = ex.Message
                };
                await dialog.ShowAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async virtual Task InitializeAsync()
        {
            await LoadArticleAsync();
        }
    }
}
