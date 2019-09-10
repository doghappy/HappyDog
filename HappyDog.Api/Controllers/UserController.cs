using HappyDog.Api.Filters;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HappyDog.Domain.Enums;
using System.Net;
using HappyDog.Domain.DataTransferObjects.User;

namespace HappyDog.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInManager"></param>
        public UserController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        readonly SignInManager<User> signInManager;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("signIn")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<HttpBaseResult> SignIn([FromBody]SignInDto dto)
        {
            var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true);
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

        /*
        [HttpPost("register")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<HttpBaseResult> Regsiter(RegisterDTO dto)
        {
            var dbUser = await userManager.FindByNameAsync(dto.UserName);
            if (dbUser == null)
            {
                var user = mapper.Map<RegisterDTO, User>(dto);
                user.PasswordHash = Guid.NewGuid();
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return new HttpBaseResult
                    {
                        Code = CodeResult.OK,
                        Notify = NotifyResult.Success,
                        Message = "注册成功"
                    };
                }
            }
            else
            {
                return new HttpBaseResult
                {
                    Code = CodeResult.Forbidden,
                    Notify = NotifyResult.Warning,
                    Message = "用户名已存在"
                };
            }
            return HttpBaseResult.NotImplemented;
        }
        */
    }
}