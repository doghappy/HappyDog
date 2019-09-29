using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HappyDog.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        readonly ILogger<HomeController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("error")]
        public HttpBaseResult Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex == null)
            {
                return NotFound();
            }
            else
            {
                _logger.LogError(ex.Error.ToString());
                return new HttpBaseResult
                {
                    Message = "服务器遇到了一个未曾预料的状况，导致了它无法完成对请求的处理。",
                    NoticeMode = NoticeMode.Danger
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("notfound")]
        public new HttpBaseResult NotFound()
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new HttpBaseResult
            {
                Message = "所请求的资源不在服务器上。",
                NoticeMode = NoticeMode.Warning
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("unauth")]
        public HttpBaseResult UnAuth()
        {
            if (Request.Query.ContainsKey("returnUrl"))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new HttpBaseResult
                {
                    Message = "所请求的资源需要身份验证。",
                    NoticeMode = NoticeMode.Warning
                };
            }
            else
            {
                return NotFound();
            }
        }
    }
}