using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models.Results;
using HappyDog.WindowsUI.Views.User;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Newtonsoft.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            set
            {
                httpClient = value;
            }
        }

        protected string BaseAddress => "http://localhost:56149";

        protected string ApplicationJson => "application/json";

        protected string DogHappy => "doghappy";

        public bool IsAuthorized => Configuration.IsAuthorized;

        protected void GoBack()
        {
            var fe = Window.Current.Content as FrameworkElement;
            var frame = fe.GetChild<Frame>("ContentFrame");
            frame.GoBack();
        }

        protected async Task HandleErrorStatusCodeAsync(HttpResponseMessage resMsg)
        {
            switch (resMsg.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    await OnUnauthorizedAsync(resMsg);
                    break;
                default:
                    await OnDefaultAsync(resMsg);
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

        protected async virtual Task OnDefaultAsync(HttpResponseMessage resMsg)
        {
            var notification = GetNotification();
            var result = await ReadHttpResponseMessageAsync(resMsg);
            notification.Show(result.Message, 4000);
        }

        protected async virtual Task OnUnauthorizedAsync(HttpResponseMessage resMsg)
        {
            await OnDefaultAsync(resMsg);
            var fe = Window.Current.Content as FrameworkElement;
            fe.GetChild<Frame>("ContentFrame").Navigate(typeof(SignInPage));
        }
    }
}
