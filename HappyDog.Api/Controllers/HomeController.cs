using System;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using HappyDog.Infrastructure.Email;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using HappyDog.Infrastructure.Handler;

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
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public HomeController(IHostingEnvironment env, IConfiguration configuration)
        {
            this.env = env;
            this.configuration = configuration;
        }

        readonly IHostingEnvironment env;
        readonly IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("error")]
        public async Task<HttpBaseResult> Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex == null)
            {
                return NotFound();
            }
            else
            {
                if (env.IsProduction())
                {
                    var sender = new OutlookSender(configuration);
                    await ExceptionHandler.SendEmailAsync(ex.Error, sender);
                }
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("throw")]
        public HttpBaseResult Throw()
        {
            throw new NotImplementedException();
        }
    }
}