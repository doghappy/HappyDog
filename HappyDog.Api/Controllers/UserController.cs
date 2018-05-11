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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        readonly SignInManager<User> signInManager;
        readonly UserManager<User> userManager;
        readonly IMapper mapper;

        public UserController(
            UserService svc,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            this.svc = svc;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost("{login}")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<HttpBaseResult> Login(LoginDto dto)
        {
            var result = await svc.LoginAsync(dto);
            if (result.Result)
            {
                await signInManager.SignInAsync(result.Data, dto.RememberMe);
                return new HttpBaseResult
                {
                    Message = "登陆成功"
                };
            }
            return new HttpBaseResult
            {
                Code = CodeResult.Unauthorized,
                Message = "密码错误",
                Notify = NotifyResult.Warning
            };
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