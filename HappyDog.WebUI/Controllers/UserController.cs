using System.Threading.Tasks;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    //public class UserController : Controller
    //{
    //    public UserController(
    //        UserService userService,
    //        SignInManager<User> signInManager,
    //        UserManager<User> userManager)
    //    {
    //        this.userService = userService;
    //        this.signInManager = signInManager;
    //        this.userManager = userManager;
    //    }

    //    readonly UserService userService;
    //    readonly SignInManager<User> signInManager;
    //    readonly UserManager<User> userManager;

    //    public IActionResult SignIn(string returnUrl)
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> SignIn(string returnUrl, SignInViewModel vm)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var result = await signInManager.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, true);
    //            if (result.Succeeded)
    //            {
    //                if (string.IsNullOrWhiteSpace(returnUrl))
    //                    return RedirectToAction("Index", "Article");
    //                else
    //                    return Redirect(returnUrl);
    //            }
    //            else
    //            {
    //                if (result.IsLockedOut)
    //                {
    //                    ModelState.AddModelError(nameof(vm.Password), "账号已锁定20分钟");
    //                }
    //                else
    //                {
    //                    ModelState.AddModelError(nameof(vm.Password), "密码错误");
    //                }
    //            }
    //        }
    //        return View(vm);
    //    }

    //    //public IActionResult SignUp(string returnUrl)
    //    //{
    //    //    return View();
    //    //}

    //    //[HttpPost]
    //    //[ValidateAntiForgeryToken]
    //    //public async Task<IActionResult> SignUp(string returnUrl, SignUpDto dto)
    //    //{
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        var user = mapper.Map<User>(dto);
    //    //        var result = await userManager.CreateAsync(user);
    //    //        if (result.Succeeded)
    //    //        {
    //    //            if (string.IsNullOrWhiteSpace(returnUrl))
    //    //            {
    //    //                return RedirectToAction("SignIn");
    //    //            }
    //    //            else
    //    //            {
    //    //                return Redirect(returnUrl);
    //    //            }
    //    //        }
    //    //    }
    //    //    return View();
    //    //}

    //    public async Task<IActionResult> SignOut()
    //    {
    //        await signInManager.SignOutAsync();
    //        return RedirectToAction("Index", "Article");
    //    }

    //    public IActionResult AccessDenied(string returnUrl)
    //    {
    //        return View();
    //    }
    //}
}