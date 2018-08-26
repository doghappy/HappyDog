using HappyDog.WindowsUI.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public abstract class ArticleViewModel : ViewModel
    {
        public ArticleViewModel()
        {
            Articles = new ObservableCollection<Models.Article>();
            HasMoreArticles = true;
        }

        public ObservableCollection<Models.Article> Articles { get; }

        private int PageNumber { get; set; }

        private int TotalPages { get; set; }

        protected abstract string Url { get; }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
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
