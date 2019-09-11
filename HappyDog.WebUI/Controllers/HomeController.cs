using HappyDog.Infrastructure.Email;
using HappyDog.Infrastructure.Handler;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHostingEnvironment env, ILogger<HomeController> logger)
        {
            _env = env;
            _logger = logger;
        }

        readonly IHostingEnvironment _env;
        readonly ILogger _logger;

        public IActionResult Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex == null)
            {
                return base.NotFound();
            }
            else
            {
                _logger.LogError(ex.Error.ToString());
                return View();
            }
        }

        public IActionResult Throw()
        {
            throw new System.Exception("熊掌不好吃");
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