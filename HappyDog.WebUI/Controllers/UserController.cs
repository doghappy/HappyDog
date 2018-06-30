using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class UserController : Controller
    {
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        readonly UserService userService;

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginAsync(dto);
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
                    await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = dto.RememberMe });
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
            }
            return View(dto);
        }
    }
}