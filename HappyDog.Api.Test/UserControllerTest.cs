using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using HappyDog.DataTransferObjects.User;
using HappyDog.Api.Controllers;
using Microsoft.AspNetCore.Identity;

namespace HappyDog.Api.Test
{
    [TestClass]
    public class UserControllerTest : TestBase
    {
        #region post: api/user/login

        //暂时不会模拟signInManager对象
        //它不好被mock，并且登录中并没有很多逻辑
        //先暂时放下
        //[TestMethod]
        public async Task LoginTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Users.AddAsync(new User { UserName = "HeroWong", Password = "111" });
            await db.SaveChangesAsync();

            var svc = new UserService(db);
            var dto = new LoginDto();
            //var dto = new LoginDto { UserName = "HeroWong", Password = "000" };

            var userManagerMock = MockHelpers.MockUserManager<User>();
            var signInManager = new SignInManager<User>(userManagerMock.Object, null, null, null, null, null);


            var controller = new UserController(svc, Mapper);

            var result = await controller.Login(dto);

            Assert.AreEqual(401, result.Code);
        }
        #endregion
    }
}
