using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
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

       
                return base.NotFound();
            }
            else
            {
                _logger.LogError(ex.Error.ToString());
                return View();
            }
        }

        //public IActionResult About()
        //{
        //    return View();
        //}

        public new IActionResult NotFound()
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View();
        }
    }
}