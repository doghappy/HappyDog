using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
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
                // log ex
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
            return new HttpBaseResult
            {
                Message = "所请求的资源不在服务器上。",
                NoticeMode = NoticeMode.Warning
            };
        }
    }
}