using HappyDog.WindowsUI.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public abstract class ArticleViewModel : ViewModel, INotifyPropertyChanged
    {
        public ArticleViewModel()
        {
            Articles = new ObservableCollection<Models.Article>();
            HasMoreArticles = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        public ObservableCollection<Models.Article> Articles { get; }

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

        protected abstract string Url { get; }

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
                string url = $"{BaseAddress}/{Url}?page={PageNumber}";
                var resMsg = await HttpClient.GetAsync(url);
                if (resMsg.IsSuccessStatusCode)
                {
                    string json = await resMsg.Content.ReadAsStringAsync();
                    var pagingData = JsonConvert.DeserializeObject<Pagination<Models.Article>>(json);
                    TotalPages = pagingData.TotalPages;
                    HasMoreArticles = PageNumber < TotalPages;
                    foreach (var item in pagingData.Data)
                    {
                        Articles.Add(item);
                    }
                }
                else
                {
                    await HandleErrorStatusCodeAsync(resMsg);
                }
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
