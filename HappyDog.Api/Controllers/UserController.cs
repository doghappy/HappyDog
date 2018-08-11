using AutoMapper;
using HappyDog.Api.Filters;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HappyDog.Domain.Enums;
using System.Net;

namespace HappyDog.Api.Controllers
{
    [Produces("application/json")]
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        public UserController(
            UserService userService,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.userService = userService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        readonly UserService userService;
        readonly SignInManager<User> signInManager;
        readonly UserManager<User> userManager;
        readonly IMapper mapper;

        [HttpPost("signIn")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<HttpBaseResult> Login([FromBody]SignInDto dto)
        {
            //var result = await userService.LoginAsync(dto);
            //if (result.Result)
            //{
            //    var dataResult = result as DataResult<User>;
            //    var claims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.NameIdentifier, dataResult.Data.Id.ToString()),
            //        new Claim(ClaimTypes.Name, dataResult.Data.UserName)
            //    };
            //    var roles = dataResult.Data.UserRoles.Select(u => u.Role);
            //    foreach (var item in roles)
            //    {
            //        claims.Add(new Claim(ClaimTypes.Role, item.Name));
            //    }
            //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //    await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = dto.RememberMe });
            //    return new HttpBaseResult
            //    {
            //        Code = CodeResult.OK,
            //        Message = result.Message
            //    };
            //}
            //return new HttpBaseResult
            //{
            //    Code = CodeResult.Unauthorized,
            //    Message = result.Message,
            //    Notify = NotifyResult.Warning
            //};

            var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true);
            if (result.Succeeded)
            {
                return new HttpBaseResult
                {
                    Status = HttpStatusCode.OK,
                    Message = "登录成功",
                    Notify = NotifyResult.Success
                };
            }
            else
            {
                if (result.IsLockedOut)
                {
                    return new HttpBaseResult
                    {
                        Status = HttpStatusCode.Forbidden,
                        Message = "账号已锁定20分钟",
                        Notify= NotifyResult.Warning
                    };
                }
                else
                {
                    return new HttpBaseResult
                    {
                        Status = HttpStatusCode.Forbidden,
                        Message = "密码错误",
                        Notify = NotifyResult.Info
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