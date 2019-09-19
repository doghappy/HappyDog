using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace HappyDog.WindowsUI.Common
{
    public static class StaticHttpClient
    {
        static HttpClient httpClient;
        public static HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                {
                    //var filter = new HttpBaseProtocolFilter();
                    //filter.IgnorableServerCertificateErrors
                    httpClient = new Windows.Web.Http.HttpClient();
                }
                return httpClient;
            }
        }

        public static Uri CombineUri(string relativeUri)
        {
            return new Uri(Configuration.BaseUri, relativeUri);
        }
    }
}
