using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex == null)
            {
                return base.NotFound();
            }
            else
            {
                // log ex
                return View();
            }
        }

        public new IActionResult NotFound()
        {
            return View();
        }
    }
}