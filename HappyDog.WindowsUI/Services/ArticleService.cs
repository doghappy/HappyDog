using HappyDog.WindowsUI.Models;
using Microsoft.Toolkit.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.Services
{
    public class ArticleService : BaseService
    {
        public ArticleService(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; }

        public async Task<Pagination<Article>> GetArticlesAsync(int page)
        {
            string url = $"article?page={page}";
            if (CategoryId > 0)
            {
                url += $"&cid={CategoryId}";
            }
            string json = await HttpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Pagination<Article>>(json);
        }

        public async Task<Article> GetArticleAsync(int id)
        {
            string url = $"article/{id}";
            string json = await HttpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Article>(json);
        }
    }
}
