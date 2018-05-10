using AutoMapper;
using HappyDog.Api.Filters;
using HappyDog.DataTransferObjects.Admin;
using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
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
        readonly HappyDogContext db;
        readonly SignInManager<User> signInManager;
        readonly UserManager<User> userManager;
        readonly IMapper mapper;

        public UserController(
            HappyDogContext db,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            this.db = db;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost("{login}")]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<HttpBaseResult> Login(LoginDTO dto)
        {
            //var user = mapper.Map<LoginDTO, User>(dto);
            var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == dto.UserName);
            if (user != null && user.Password == HashEncrypt.Sha1Encrypt($"HappyDog-{dto.Password}-{user.PasswordHash}"))
            {
                await signInManager.SignInAsync(user, dto.RememberMe);
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

        //[AllowAnonymous]
        public string Get()
        {
            //throw new Exception("ts");
            return "test";
        }
        
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
                    //var code = await userManager.GenerateEmailConfirmationTokenAsync(account);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = account.Id, code }, HttpContext.Request.Scheme);
                    //await 

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
    }
}