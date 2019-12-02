using System.Net;
using System.Threading.Tasks;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.Console.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        readonly SignInManager<User> _signInManager;

        [HttpPost("signin")]
        public async Task<HttpBaseResult> SignIn([FromBody]SignInDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true);
            if (result.Succeeded)
            {
                return new HttpBaseResult
                {
                    Message = "登录成功",
                    NoticeMode = NoticeMode.Success
                };
            }
            else
            {
                if (result.IsLockedOut)
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return new HttpBaseResult
                    {
                        Message = "账号已锁定20分钟",
                        NoticeMode = NoticeMode.Warning
                    };
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return new HttpBaseResult
                    {
                        Message = "密码错误",
                        NoticeMode = NoticeMode.Info
                    };
                }
            }
        }

        [HttpPost("signout")]
        public async Task<HttpBaseResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return new HttpBaseResult
            {
                NoticeMode = NoticeMode.Success,
                Message = "您已注销"
            };
        }
    }
}