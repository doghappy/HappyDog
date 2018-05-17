using AutoMapper;
using HappyDog.Api.Filters;
using HappyDog.DataTransferObjects.User;
using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HappyDog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        readonly UserService svc;
        readonly IMapper mapper;

        public UserController(
            UserService svc,
            IMapper mapper)
        {
            this.svc = svc;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<HttpBaseResult> Login([FromBody]LoginDto dto)
        {
            var result = await svc.LoginAsync(dto);
            if (result.Result)
            {
                var dataResult = result as DataResult<User>;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, dataResult.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name, dataResult.Data.UserName)
                };
                var roles = dataResult.Data.UserRoles.Select(u => u.Role);
                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Name));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //AuthenticationProperties authProps = new AuthenticationProperties();
                //if (dto.RememberMe)
                //{
                //    authProps.IsPersistent = true;
                //}
                //else
                //{
                //    authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20);
                //    authProps.oli
                //}

                await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = dto.RememberMe });
                return new HttpBaseResult
                {
                    Code = CodeResult.OK,
                    Message = result.Message
                };
            }
            return new HttpBaseResult
            {
                Code = CodeResult.Unauthorized,
                Message = result.Message,
                Notify = NotifyResult.Warning
            };
        }

        public string Get()
        {
            return "get";
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