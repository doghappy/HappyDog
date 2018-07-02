using System.Net.Http;

namespace HappyDog.WindowsUI.Services
{
    public class HttpClient
    {
        static System.Net.Http.HttpClient _client;

        public static System.Net.Http.HttpClient Instance
        {
            get
            {
                if (_client == null)
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                    };
                    _client = new System.Net.Http.HttpClient(handler);
                }
                return _client;
            }
        }
    }
}
