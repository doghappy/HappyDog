using HappyDog.WindowsUI.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HappyDog.WindowsUI.Services
{
    public class ArticleService : BaseService
    {
        public async Task<Pagination<Article>> GetArticlesAsync(int page, int? cid = null)
        {
            string url = $"article?page={page}";
            if (cid.HasValue)
            {
                url += $"cid={cid.Value}";
            }
            string json = await HttpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Pagination<Article>>(json);
        }
    }
}
