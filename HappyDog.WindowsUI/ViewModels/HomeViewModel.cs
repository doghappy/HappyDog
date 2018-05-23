using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Articles = new ObservableCollection<Article>();
        }

        public ObservableCollection<Article> Articles { get; }

        private async Task LoadArticleAsync()
        {
            var svc = new ArticleService();
            var articles = await svc.GetArticlesAsync(1);
            foreach (var item in articles.Data)
            {
                Articles.Add(item);
            }
        }

        public async Task InitializeAsync()
        {
            await LoadArticleAsync();
        }
    }
}
