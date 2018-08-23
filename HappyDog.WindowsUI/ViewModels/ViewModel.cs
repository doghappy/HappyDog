using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models.Results;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Newtonsoft.Json;
using Windows.UI.Xaml;

namespace HappyDog.WindowsUI.ViewModels
{
    public abstract class ViewModel
    {
        static HttpClient httpClient;

        protected static HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                {
                    httpClient = new HttpClient();
                }
                return httpClient;
            }
        }

        protected string BaseAddress => "http://localhost:56149";

        protected async Task ParseStatusCodeAsync(HttpResponseMessage resMsg)
        {
            switch (resMsg.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    await OnNotFoundAsync(resMsg);
                    break;
            }
        }

        protected virtual InAppNotification GetNotification()
        {
            var fe = Window.Current.Content as FrameworkElement;
            return fe.GetChild<InAppNotification>("MainNotification");
        }

        protected async virtual Task<HttpBaseResult> ReadHttpResponseMessageAsync(HttpResponseMessage resMsg)
        {
            string json = await resMsg.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HttpBaseResult>(json);
        }

        protected async virtual Task OnNotFoundAsync(HttpResponseMessage resMsg)
        {
            var notification = GetNotification();
            var result = await ReadHttpResponseMessageAsync(resMsg);
            notification.Show(result.Message, 4000);
        }
    }
}
