using HappyDog.Api.Controllers;
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

namespace HappyDog.Api.Test
{
    [TestClass]
    public class ArticleControllerTest : TestBase
    {
        #region get: api/article/{id}

        [TestMethod]
        public async Task NonAdminGetDisableTest()
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
        public async Task NonAdminGetEnableTest()
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
        }

        [TestMethod]
        public async Task AdminGetDisableTest()
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
        }

        [TestMethod]
        public async Task AdminGetEnableTest()
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
        }

        #endregion

        #region get: api/article

        #region Non Admin
        [TestMethod]
        public async Task NonAdminGetManyWithNoCidAndDefaultPageIndex()
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
        public async Task NonAdminGetManyWithNoCidAndPageIndexEq4()
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
        public async Task NonAdminGetManyWithCidEq1AndDefaultPageIndex()
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

            var result = await controller.Get(1, 1);
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(7, data[0].Id);
        }

        [TestMethod]
        public async Task NonAdminGetManyWithCidEq1AndPageIndexEq2()
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

            var result = await controller.Get(1, 2);
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(3, data[0].Id);
        }
        #endregion

        #region Admin
        [TestMethod]
        public async Task AdminGetManyWithNoCidAndDefaultPageIndex()
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
        public async Task AdminGetManyWithNoCidAndPageIndexEq4()
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
        public async Task AdminGetManyWithCidEq1AndDefaultPageIndex()
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

            var result = await controller.Get(1, 1);
            Assert.AreEqual(4, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(7, data[0].Id);
        }

        [TestMethod]
        public async Task AdminGetManyWithCidEq1AndPageIndexEq2()
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

            var result = await controller.Get(1, 2);
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

            var dto = new PutArticleDto { Title = "title2", Content = "content2", CategoryId = 2, State = BaseState.Enable };
            await controller.Put(1, dto);

            Assert.AreEqual(1, article.Id);
            Assert.AreEqual("title2", article.Title);
            Assert.AreEqual("content2", article.Content);
            Assert.AreEqual(BaseState.Enable, article.State);
            Assert.AreEqual(2, article.CategoryId);
        }
        #endregion
    }
}
