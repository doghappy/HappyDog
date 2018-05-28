using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.Models.Results;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HappyDog.WindowsUI.Services
{
    public class UserService : BaseService
    {
        public async Task<HttpBaseResult> LoginAsync(string userName, string password)
        {
            var data = new
            {
                UserName = userName,
                Password = password,
                RememberMe = true
            };
            string json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, ApplicationJson);
            var resMsg = await HttpClient.PostAsync("user/login", content);
            var resJson = await resMsg.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<HttpBaseResult>(resJson);
            if (result.Code == CodeResult.OK)
            {
                string cookie = resMsg.Headers.GetValues("Set-Cookie").FirstOrDefault(c => c.StartsWith(".AspNetCore.Cookies"));
                ApplicationData.Current.LocalSettings.Values["AuthCookie"] = cookie;
            }
            return result;
        }
    }
}
