using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HappyDog.WebUI.Test;
using HappyDog.WebUI.Controllers.Api;

namespace HappyDog.Api.Test
{
    [TestClass]
    public class ApiArticleControllerTest : TestBase
    {
        #region get: api/article/{id}

        [TestMethod]
        public async Task NonAuthGetDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable });
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
        public async Task NonAuthGetEnableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = new Category() });
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
            var data = result.Value as ArticleDto;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Id);
            Assert.AreEqual(1, data.ViewCount);
        }

        [TestMethod]
        public async Task GetDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = new Category() });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(true);
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

            var result = (await controller.Get(1)) as JsonResult;
            var data = result.Value as ArticleDto;
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Id);
            Assert.AreEqual(0, data.ViewCount);
        }

        [TestMethod]
        public async Task GetEnableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = new Category() });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(true);
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
            var data = result.Value as ArticleDto;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Id);
            Assert.AreEqual(1, data.ViewCount);
        }

        #endregion

        #region get: api/article

        #region Non Auth
        [TestMethod]
        public async Task NonAuthGetManyWithNoCidAndDefaultPageIndex()
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
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(null);
            Assert.AreEqual(7, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(8, data[0].Id);
        }

        [TestMethod]
        public async Task NonAuthGetManyWithNoCidAndPageIndexEq4()
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
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(null, 4);
            Assert.AreEqual(7, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(2, data[0].Id);
        }

        [TestMethod]
        public async Task NonAuthGetManyWithCidEq1AndDefaultPageIndex()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", State = BaseState.Enable, Category = c2 });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(false);
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(ArticleCategory.Net, 1);
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(7, data[0].Id);
        }

        [TestMethod]
        public async Task NonAuthGetManyWithCidEq1AndPageIndexEq2()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", State = BaseState.Enable, Category = c2 });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(false);
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(ArticleCategory.Net, 2);
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(3, data[0].Id);
        }
        #endregion

        #region Has Auth
        [TestMethod]
        public async Task GetManyWithNoCidAndDefaultPageIndex()
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
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(true);
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(null);
            Assert.AreEqual(8, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(8, data[0].Id);
        }

        [TestMethod]
        public async Task GetManyWithNoCidAndPageIndexEq4()
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
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(true);
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(null, 4);
            Assert.AreEqual(8, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(2, data[0].Id);
        }

        [TestMethod]
        public async Task GetManyWithCidEq1AndDefaultPageIndex()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", State = BaseState.Enable, Category = c2 });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(true);
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(ArticleCategory.Net, 1);
            Assert.AreEqual(4, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(7, data[0].Id);
        }

        [TestMethod]
        public async Task GetManyWithCidEq1AndPageIndexEq2()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", State = BaseState.Disable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", State = BaseState.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", State = BaseState.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", State = BaseState.Enable, Category = c2 });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(true);
            var controller = new ArticleController(svc, Mapper)
            {
                PageSize = 2,
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = await controller.Get(ArticleCategory.Net, 2);
            Assert.AreEqual(4, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(1, data[1].Id);
        }
        #endregion

        #endregion

        #region put: api/article/{id}
        [TestMethod]
        public async Task PutTest()
        {
            var db = new HappyDogContext(GetOptions());
            var article = new Article { Id = 1, Title = "title1", Content = "content1", CategoryId = 1, State = BaseState.Disable };
            await db.AddAsync(article);
            await db.SaveChangesAsync();

            var svc = new ArticleService(db);
            var controller = new ArticleController(svc, null);

            var dto = new EditArticleDto { Title = "title2", Content = "content2", CategoryId = 2, State = BaseState.Enable };
            await controller.Put(1, dto);

            Assert.AreEqual(1, article.Id);
            Assert.AreEqual("title2", article.Title);
            Assert.AreEqual("content2", article.Content);
            Assert.AreEqual(BaseState.Enable, article.State);
            Assert.AreEqual(2, article.CategoryId);
        }
        #endregion

        #region post: api/article
        [TestMethod]
        public async Task PostAsync()
        {
            var db = new HappyDogContext(GetOptions());
            var svc = new ArticleService(db);
            var controller = new ArticleController(svc, null);

            var dto = new PostArticleDto { Title = "title", Content = "content", CategoryId = 1, State = BaseState.Enable };
            await controller.Post(dto);

            var list = db.Articles.ToList();
            Assert.AreEqual(1, list.Count);

            var article = list.FirstOrDefault();
            Assert.AreEqual("title", article.Title);
            Assert.AreEqual("content", article.Content);
            Assert.AreEqual(1, article.CategoryId);
            Assert.AreEqual(BaseState.Enable, article.State);
        }
        #endregion
    }
}
