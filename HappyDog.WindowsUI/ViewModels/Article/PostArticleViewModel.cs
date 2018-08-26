using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public class PostArticleViewModel : ViewModel, INotifyPropertyChanged
    {
        public PostArticleViewModel()
        {
            Article = new Models.Article
            {
                Category = Categories.FirstOrDefault()
            };
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Category> Categories => Configuration.Categories;

        private Models.Article article;
        public Models.Article Article
        {
            get => article;
            set
            {
                article = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Article)));
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task PostAsync()
        {
            Article.CategoryId = Article.Category.Id;
            string url = BaseAddress + "/article";
            string json = JsonConvert.SerializeObject(Article);
            var content = new StringContent(json, Encoding.UTF8, ApplicationJson);
            var resMsg = await HttpClient.PostAsync(url, content);
            if (resMsg.IsSuccessStatusCode)
            {
                string sss = await resMsg.Content.ReadAsStringAsync();
                Configuration.ClearCache();
                GoBack();
            }
            else
            {
                await HandleErrorStatusCodeAsync(resMsg);
            }
        }
    }
}
