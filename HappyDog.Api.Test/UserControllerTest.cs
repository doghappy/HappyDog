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
using Microsoft.AspNetCore.Identity;

namespace HappyDog.Api.Test
{
    [TestClass]
    public class UserControllerTest : TestBase
    {
        #region post: api/user/login

        [TestMethod]
        public async Task CorrectPasswordTest()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var signInManagerMock = new Mock<SignInManager<User>>(mockUserManager.Object, mockContextAccessor.Object, mockUserPrincipalFactory.Object, null, null, null);
            signInManagerMock
                .Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new SignInResultWrapper(true, false) as Microsoft.AspNetCore.Identity.SignInResult));

            var controller = new UserController(signInManagerMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = await controller.SignIn(new SignInDto());
            Assert.AreEqual(NoticeMode.Success, result.NoticeMode);
            Assert.AreEqual("登录成功", result.Message);
        }

        [TestMethod]
        public async Task IncorrectPasswordTest()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var signInManagerMock = new Mock<SignInManager<User>>(mockUserManager.Object, mockContextAccessor.Object, mockUserPrincipalFactory.Object, null, null, null);
            signInManagerMock
                .Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new Microsoft.AspNetCore.Identity.SignInResult()));

            var controller = new UserController(signInManagerMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = await controller.SignIn(new SignInDto());
            Assert.AreEqual(NoticeMode.Info, result.NoticeMode);
            Assert.AreEqual("密码错误", result.Message);
        }

        [TestMethod]
        public async Task LockedTest()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var signInManagerMock = new Mock<SignInManager<User>>(mockUserManager.Object, mockContextAccessor.Object, mockUserPrincipalFactory.Object, null, null, null);
            signInManagerMock
                .Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new SignInResultWrapper(false, true) as Microsoft.AspNetCore.Identity.SignInResult));

            var controller = new UserController(signInManagerMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = await controller.SignIn(new SignInDto());
            Assert.AreEqual(NoticeMode.Warning, result.NoticeMode);
            Assert.AreEqual("账号已锁定20分钟", result.Message);
        }
        #endregion
    }

    class SignInResultWrapper : Microsoft.AspNetCore.Identity.SignInResult
    {
        public SignInResultWrapper(bool succeeded, bool isLockedOut)
        {
            IsLockedOut = isLockedOut;
            Succeeded = succeeded;
        }
    }
}
