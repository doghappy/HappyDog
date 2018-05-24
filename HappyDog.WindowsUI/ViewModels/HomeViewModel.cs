using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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
            try
            {
                var articles = await svc.GetArticlesAsync(1);
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
        }

        public async Task InitializeAsync()
        {
            await LoadArticleAsync();
        }
    }
}
