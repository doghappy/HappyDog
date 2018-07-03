using System;

namespace HappyDog.WindowsUI.Services
{
    public abstract class BaseService
    {
        protected string BaseUrl
        {
            get
            {
#if DEBUG
                return "http://localhost:56149";
#else
                return "https://api.doghappy.wang";
# endif
            }
        }

        protected System.Net.Http.HttpClient HttpClient
        {
            get
            {
                if (Services.HttpClient.Instance.BaseAddress == null)
                {
                    Services.HttpClient.Instance.BaseAddress = new Uri(BaseUrl);
                }
                return Services.HttpClient.Instance;
            }
        }

        protected string ApplicationJson => "application/json";
    }
}
