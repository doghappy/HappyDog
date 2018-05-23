namespace HappyDog.WindowsUI.Services
{
    public class HttpClient
    {
        static System.Net.Http.HttpClient _client;

        public static System.Net.Http.HttpClient Instance => _client ?? (_client = new System.Net.Http.HttpClient());
    }
}
