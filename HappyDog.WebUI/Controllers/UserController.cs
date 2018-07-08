using System.Threading.Tasks;
using AutoMapper;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
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

        public IActionResult SignIn(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string returnUrl, SignInDto dto)
        {
            //if (ModelState.IsValid)
            //{
            //    var result = await userService.LoginAsync(dto);
            //    if (result.Result)
            //    {
            //        var dataResult = result as DataResult<User>;
            //        var claims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.NameIdentifier, dataResult.Data.Id.ToString()),
            //            new Claim(ClaimTypes.Name, dataResult.Data.UserName)
            //        };
            //        var roles = dataResult.Data.UserRoles.Select(u => u.Role);
            //        foreach (var item in roles)
            //        {
            //            claims.Add(new Claim(ClaimTypes.Role, item.Name));
            //        }
            //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //        await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = dto.RememberMe });
            //        if (string.IsNullOrWhiteSpace(returnUrl))
            //        {
            //            return RedirectToAction("Index", "Article");
            //        }
            //        else
            //        {
            //            return Redirect(returnUrl);
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Message = result.Message;
            //    }
            //}
            //return View(dto);

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true);
                if (result.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Index", "Article");
                    else
                        return Redirect(returnUrl);
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(nameof(dto.Password), "账号已锁定20分钟");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(dto.Password), "密码错误");
                    }
                }
            }
            return View(dto);
        }

        public IActionResult SignUp(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(string returnUrl, SignUpDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<User>(dto);
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("SignIn");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return View();
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }
    }
}