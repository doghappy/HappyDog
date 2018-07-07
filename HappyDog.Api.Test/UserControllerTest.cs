using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using HappyDog.Domain.DataTransferObjects.User;
using HappyDog.Api.Controllers;
using Moq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Mvc;
using HappyDog.Domain.Enums;

namespace HappyDog.Api.Test
{
    [TestClass]
    public class UserControllerTest : TestBase
    {
        #region post: api/user/login
        
        [TestMethod]
        public async Task ErrorLoginInfoLoginTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Users.AddAsync(new User { UserName = "HeroWong", PasswordHash = "111" });
            await db.SaveChangesAsync();

            var svc = new UserService(db);
            var dto = new SignInDto();
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(s => s.GetService(typeof(IServiceProvider)))
                .Returns(authServiceMock.Object);

            var controller = new UserController(svc, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result = await controller.Login(dto);
            Assert.AreEqual(CodeResult.Unauthorized, result.Code);
        }

        [TestMethod]
        public async Task CorrectInfoLoginTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Users.AddAsync(new User { UserName = "HeroWong", PasswordHash = "111" });
            await db.SaveChangesAsync();

            var svc = new UserService(db);
            var dto = new SignInDto { UserName = "HeroWong", Password = "111" };
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(s => s.GetService(typeof(IAuthenticationService))).Returns(authServiceMock.Object);

            var controller = new UserController(svc, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result = await controller.Login(dto);
            Assert.AreEqual(CodeResult.OK, result.Code);
        }
        #endregion
    }
}
