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
        public async Task OwerGetDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable });
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

            var result = (await controller.Detail(1)) as NotFoundResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task OwerGetEnableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = (await controller.Detail(2)) as JsonResult;
            var data = result.Value as ArticleDto;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Id);
            Assert.AreEqual(1, data.ViewCount);
        }

        [TestMethod]
        public async Task GetDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = (await controller.Detail(1)) as JsonResult;
            var data = result.Value as ArticleDto;
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Id);
            Assert.AreEqual(0, data.ViewCount);
        }

        [TestMethod]
        public async Task GetEnableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = (await controller.Detail(2)) as JsonResult;
            var data = result.Value as ArticleDto;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Id);
            Assert.AreEqual(1, data.ViewCount);
        }

        #endregion

        #region get: api/article

        #region Non Onwer Get
        [TestMethod]
        public async Task OwerGetListWithDefaultPageIndex()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = await controller.List();
            Assert.AreEqual(7, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(8, data[0].Id);
        }

        [TestMethod]
        public async Task OwerGetListWithPageIndexEq4()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = await controller.List(4);
            Assert.AreEqual(7, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(1, data.Count);
            Assert.AreEqual(2, data[0].Id);
        }

        [TestMethod]
        public async Task OwerGetListWithOutRangePageIndex()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = await controller.List(5);
            Assert.AreEqual(7, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        public async Task OwerGetNet()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c1 });
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

            var result = await controller.Net();
            Assert.AreEqual(2, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task OwerGetDatabase()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c2 });
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

            var result = await controller.Database();
            Assert.AreEqual(2, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task OwerGetWindows()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c3 = new Category { Id = 3 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c3 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c3 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c3 });
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

            var result = await controller.Windows();
            Assert.AreEqual(2, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task OwerGetRead()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c4 = new Category { Id = 4 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c4 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c4 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c4 });
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

            var result = await controller.Read();
            Assert.AreEqual(2, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task OwerGetEssays()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c5 = new Category { Id = 5 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c5 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c5 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c5 });
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

            var result = await controller.Essays();
            Assert.AreEqual(2, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }
        #endregion

        #region Onwer Get
        [TestMethod]
        public async Task GetListWithDefaultPageIndex()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = await controller.List();
            Assert.AreEqual(8, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(8, data[0].Id);
        }

        [TestMethod]
        public async Task GetListWithPageIndexEq4()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = await controller.List(4);
            Assert.AreEqual(8, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(1, data[1].Id);
        }

        [TestMethod]
        public async Task GetListWithOutRangePageIndex()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
            await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
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

            var result = await controller.List(5);
            Assert.AreEqual(8, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        public async Task GetNet()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c1 });
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

            var result = await controller.Net();
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task GetDatabase()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c2 = new Category { Id = 2 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c2 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c2 });
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

            var result = await controller.Database();
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task GetWindows()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c3 = new Category { Id = 3 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c3 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c3 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c3 });
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

            var result = await controller.Windows();
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task GetRead()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c4 = new Category { Id = 4 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c4 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c4 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c4 });
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

            var result = await controller.Read();
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }

        [TestMethod]
        public async Task GetEssays()
        {
            var db = new HappyDogContext(GetOptions());
            var c1 = new Category { Id = 1 };
            var c5 = new Category { Id = 5 };
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c5 });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c5 });
            await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c5 });
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

            var result = await controller.Essays();
            Assert.AreEqual(3, result.TotalItems);

            var data = result.Data.ToList();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(4, data[0].Id);
        }
        #endregion

        #endregion

        #region put: api/article/{id}
        [TestMethod]
        public async Task PutTest()
        {
            var db = new HappyDogContext(GetOptions());
            var article = new Article { Id = 1, Title = "title1", Content = "content1", CategoryId = 1, Status = BaseStatus.Disable };
            await db.AddAsync(article);
            await db.SaveChangesAsync();

            var svc = new ArticleService(db);
            var controller = new ArticleController(svc, null);

            var dto = new EditArticleDto { Title = "title2", Content = "content2", CategoryId = 2, Status = BaseStatus.Enable };
            await controller.Put(1, dto);

            Assert.AreEqual(1, article.Id);
            Assert.AreEqual("title2", article.Title);
            Assert.AreEqual("content2", article.Content);
            Assert.AreEqual(BaseStatus.Enable, article.Status);
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

            var dto = new PostArticleDto { Title = "title", Content = "content", CategoryId = 1, Status = BaseStatus.Enable };
            await controller.Post(dto);

            var list = db.Articles.ToList();
            Assert.AreEqual(1, list.Count);

            var article = list.FirstOrDefault();
            Assert.AreEqual("title", article.Title);
            Assert.AreEqual("content", article.Content);
            Assert.AreEqual(1, article.CategoryId);
            Assert.AreEqual(BaseStatus.Enable, article.Status);
        }
        #endregion
    }
}