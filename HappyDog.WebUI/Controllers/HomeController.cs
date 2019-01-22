using HappyDog.Infrastructure.Email;
using HappyDog.Infrastructure.Handler;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHostingEnvironment env, IConfiguration configuration)
        {
            this.env = env;
            this.configuration = configuration;
        }

        readonly IHostingEnvironment env;
        readonly IConfiguration configuration;

        public async Task<IActionResult> Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex == null)
            {
                return base.NotFound();
            }
            else
            {
                if (env.IsProduction())
                {
                    var sender = new OutlookSender(configuration);
                    await ExceptionHandler.SendEmailAsync(ex.Error, sender);
                }
                return View();
            }
        }

        public new IActionResult NotFound()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}