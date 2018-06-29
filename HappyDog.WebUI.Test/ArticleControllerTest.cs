using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using HappyDog.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class ArticleControllerTest : TestBase
    {
        [TestMethod]
        public async Task IndexNonAuthGetDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", State = BaseState.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", State = BaseState.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", State = BaseState.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", State = BaseState.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", State = BaseState.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", State = BaseState.Enable, Category = new Category() });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(false);
            var controller = new ArticleController(svc)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                },
                PageSize = 2
            };

            var result = (await controller.Index()) as ViewResult;
            var model = result.Model as List<Article>;
            var pager = result.ViewData["Pager"] as Pager;

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(8, model[0].Id);
            Assert.AreEqual(7, pager.TotalItems);
            Assert.AreEqual(4, pager.TotalPages);
        }
    }
}
