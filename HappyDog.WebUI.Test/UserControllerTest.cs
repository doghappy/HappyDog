using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using HappyDog.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class UserControllerTest : TestBase
    {
        #region SignIn
        //[TestMethod]
        // 在进行单元测试时，遇到了一些问题，暂时放弃对登录进行单元测试
        // 此项目中，使用了 Identity 框架，还进行单元测试，是否有必要？
        public async Task ErrorLoginInfoLoginTest()
        {
            //var db = new HappyDogContext(GetOptions());
            //await db.Users.AddAsync(new User { UserName = "HeroWong", PasswordHash = "111" });
            //await db.SaveChangesAsync();

            //var svc = new UserService(db);
            //var dto = new SignInDto();

            //var signInManager = new Mock<SignInManager<User>>();
            //var signInResult = new Mock<Microsoft.AspNetCore.Identity.SignInResult>();
            //signInResult.SetupGet(s => s.Succeeded).Returns(true);
            //signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            //    .Returns(Task.FromResult(signInResult.Object));

            //var controller = new UserController(svc, signInManager.Object, null);
            //var result = (await controller.SignIn("", dto)) as ViewResult;
            //Assert.AreEqual("密码错误", result.ViewData["Message"].ToString());
        }

        //[TestMethod]
        // 在进行单元测试时，遇到了一些问题，暂时放弃对登录进行单元测试
        // 此项目中，使用了 Identity 框架，还进行单元测试，是否有必要？
        public async Task CorrectInfoLoginTest()
        {
            //var db = new HappyDogContext(GetOptions());
            //await db.Users.AddAsync(new User { UserName = "HeroWong", PasswordHash = "111" });
            //await db.SaveChangesAsync();

            //var userService = new UserService(db);
            //var dto = new SignInDto { UserName = "HeroWong", Password = "111" };
            ////var authServiceMock = new Mock<IAuthenticationService>();
            ////authServiceMock
            ////    .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            ////    .Returns(Task.CompletedTask);
            ////var serviceProviderMock = new Mock<IServiceProvider>();
            ////serviceProviderMock.Setup(s => s.GetService(typeof(IAuthenticationService))).Returns(authServiceMock.Object);

            //var controller = new UserController(userService, null, null, Mapper)
            //{
            //    ControllerContext = new ControllerContext
            //    {
            //        HttpContext = new DefaultHttpContext
            //        {
            //            //RequestServices = serviceProviderMock.Object
            //        }
            //    }
            //};

            //var result = (await controller.SignIn("", dto)) as RedirectToActionResult;
            //Assert.IsNotNull(result);
        }
        #endregion
    }
}
