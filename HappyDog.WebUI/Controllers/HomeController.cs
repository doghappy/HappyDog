using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HappyDog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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