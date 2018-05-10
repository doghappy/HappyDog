using HappyDog.Api.Controllers;
using HappyDog.DataTransferObjects.Article;
using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HappyDog.Api.Test
{
    [TestClass]
    public class ArticleControllerTest : TestBase
    {
        #region get: api/article

        [TestMethod]
        public async Task UnLoginGetDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            db.Articles.Add(new Article { Id = 1, Title = "article 1", State = BaseState.Disable });
            db.Articles.Add(new Article { Id = 2, Title = "article 2", State = BaseState.Enable });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(false);
            var controller = new ArticleController(svc, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = (await controller.Get(1)) as NotFoundResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task UnLoginGetEnableTest()
        {
            var db = new HappyDogContext(GetOptions());
            db.Articles.Add(new Article { Id = 1, Title = "article 1", State = BaseState.Disable });
            db.Articles.Add(new Article { Id = 2, Title = "article 2", State = BaseState.Enable });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(false);
            var controller = new ArticleController(svc, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = (await controller.Get(2)) as JsonResult;
            Assert.IsNotNull(result);
            //Assert.AreEqual(200, result.StatusCode);

            var data = result.Value as ArticleDTO;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Id);
        }

        #endregion
    }
}
