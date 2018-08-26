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
            Category = Categories.FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public async Task PostAsync()
        {
            string url = BaseAddress + "/article";
            string json = JsonConvert.SerializeObject(Article);
            var resMsg = await HttpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, ApplicationJson));
            if (resMsg.IsSuccessStatusCode)
            {
                GoBack();
            }
            else
            {
                await HandleErrorStatusCodeAsync(resMsg);
            }
        }
    }
}
