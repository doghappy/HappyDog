using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Models.Results;
using Microsoft.Toolkit.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.Services
{
    public class ArticleService : BaseService
    {
        public ArticleService() { }

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

        public async Task<HttpDataResult<int>> PostAsync(Article article)
        {
            string url = $"article";
            string json = JsonConvert.SerializeObject(article);
            var content = new StringContent(json, Encoding.UTF8, ApplicationJson);
            var resMsg = await HttpClient.PostAsync(url, content);
            string resJson = await resMsg.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HttpDataResult<int>>(resJson);
        }
    }
}
