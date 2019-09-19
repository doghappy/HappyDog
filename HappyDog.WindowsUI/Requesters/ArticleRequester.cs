using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Models.Results;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.Requesters
{
    class ArticleRequester
    {
        public async Task<Pagination<Article>> GetArticlesAsync(int page = 1)
        {
            Uri uri = new Uri(Configuration.BaseUri, $"api/article?page={page}");
            string json = await StaticHttpClient.HttpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<Pagination<Article>>(json);
        }

        public async Task<Pagination<Article>> GetArticlesAsync(Enums.Category category, int page = 1)
        {
            string categoryStr = GetCategoryForUrl(category);
            Uri uri = new Uri(Configuration.BaseUri, $"api/article/{categoryStr}?page={page}");
            string json = await StaticHttpClient.HttpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<Pagination<Article>>(json);
        }

        public async Task<Article> GetArticleAsync(int id)
        {
            Uri uri = new Uri(Configuration.BaseUri, $"api/article/{id}");
            string json = await StaticHttpClient.HttpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<Article>(json);
        }

        public async Task<HttpDataResult<Pagination<Article>>> SearchArticlesAsync(string q, int page = 1)
        {
            Uri uri = new Uri(Configuration.BaseUri, $"api/article/search?q={q}&page={page}");
            string json = await StaticHttpClient.HttpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<HttpDataResult<Pagination<Article>>>(json);
        }

        private string GetCategoryForUrl(Enums.Category category)
        {
            string result = string.Empty;
            switch (category)
            {
                case Enums.Category.DotNet:
                    result = "net";
                    break;
                case Enums.Category.Database:
                    result = "db";
                    break;
                case Enums.Category.Windows:
                    result = "windows";
                    break;
                case Enums.Category.Read:
                    result = "read";
                    break;
                case Enums.Category.Essays:
                    result = "essays";
                    break;
                default:
                    throw new NotImplementedException();
            }
            return result;
        }
    }
}
