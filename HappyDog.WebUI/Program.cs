using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HappyDog.WebUI
{
    public class Program
    {
       public static string Version { get; private set; }

        public static void Main(string[] args)
        {
            Version = typeof(Program).Assembly.GetName().Version.ToString();
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
