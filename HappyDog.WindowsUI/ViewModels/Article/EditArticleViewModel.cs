using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Models.Results;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels.Article
{
    public class EditArticleViewModel : ViewModel, INotifyPropertyChanged
    {
        public EditArticleViewModel(Models.Article article)
        {
            Article = article;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public async Task PutAsync()
        {
            string url = BaseAddress + "/article/" + Article.Id;
            string json = JsonConvert.SerializeObject(Article);
            var content = new StringContent(json, Encoding.UTF8, ApplicationJson);
            var resMsg = await HttpClient.PutAsync(url, content);
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
