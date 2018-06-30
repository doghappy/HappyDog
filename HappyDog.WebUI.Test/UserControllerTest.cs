using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using HappyDog.WebUI.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class UserControllerTest : TestBase
    {
        #region SignIn
        [TestMethod]
        public async Task ErrorLoginInfoLoginTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Users.AddAsync(new User { UserName = "HeroWong", Password = "111" });
            await db.SaveChangesAsync();

            var svc = new UserService(db);
            var dto = new SignInDto();
            //var authServiceMock = new Mock<IAuthenticationService>();
            //authServiceMock.Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            //    .Returns(Task.CompletedTask);
            //var serviceProviderMock = new Mock<IServiceProvider>();
            //serviceProviderMock.Setup(s => s.GetService(typeof(IServiceProvider)))
            //    .Returns(authServiceMock.Object);

            var controller = new UserController(svc)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        //RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result = (await controller.SignIn(dto)) as ViewResult;
            Assert.AreEqual("密码错误", result.ViewData["Message"].ToString());
        }

        [TestMethod]
        public async Task CorrectInfoLoginTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Users.AddAsync(new User { UserName = "HeroWong", Password = "111" });
            await db.SaveChangesAsync();

            var userService = new UserService(db);
            var dto = new SignInDto { UserName = "HeroWong", Password = "111" };
            //var authServiceMock = new Mock<IAuthenticationService>();
            //authServiceMock
            //    .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            //    .Returns(Task.CompletedTask);
            //var serviceProviderMock = new Mock<IServiceProvider>();
            //serviceProviderMock.Setup(s => s.GetService(typeof(IAuthenticationService))).Returns(authServiceMock.Object);

            var controller = new UserController(userService)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        //RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result = (await controller.SignIn(dto)) as RedirectToActionResult;
            Assert.IsNotNull(result);
        }
        #endregion
    }
}
