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
using System.Collections.Generic;
using HappyDog.Domain.Models.Results;
using HappyDog.Infrastructure;
using Microsoft.EntityFrameworkCore;

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
        public async Task PutNullTest()
        {
            var db = new HappyDogContext(GetOptions());
            var article = new Article { Id = 1, Title = "title1", Content = "content1", CategoryId = 1, Status = BaseStatus.Disable };
            await db.AddAsync(article);
            await db.SaveChangesAsync();

            var articleService = new ArticleService(db);
            var controller = new ArticleController(articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var dto = new EditArticleDto(db) { Title = "title2", Content = "content2", Status = BaseStatus.Enable };
            var result = await controller.Put(2, dto);

            Assert.AreEqual(1, article.Id);
            Assert.AreEqual("title1", article.Title);
            Assert.AreEqual("content1", article.Content);
            Assert.AreEqual(BaseStatus.Disable, article.Status);
            Assert.AreEqual(1, article.CategoryId);
            Assert.AreEqual(NoticeMode.Warning, result.NoticeMode);
            Assert.AreEqual("修改失败", result.Message);
        }

        [TestMethod]
        public async Task PutTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 2, Status = BaseStatus.Enable });
            await db.AddAsync(new Article { Id = 1, Title = "title1", Content = "content1", CategoryId = 1, Status = BaseStatus.Disable });
            await db.SaveChangesAsync();

            var svc = new ArticleService(db);
            var controller = new ArticleController(svc, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var dto = new EditArticleDto(db) { Title = "title2", Content = "content2", CategoryId = 2, Status = BaseStatus.Enable };
            var result = await controller.Put(1, dto);
            var dbArticle = await db.Articles.FirstOrDefaultAsync();

            Assert.AreEqual(NoticeMode.Success, result.NoticeMode);
            Assert.AreEqual("修改成功", result.Message);
            Assert.AreEqual(1, dbArticle.Id);
            Assert.AreEqual("title2", dbArticle.Title);
            Assert.AreEqual("content2", dbArticle.Content);
            Assert.AreEqual(BaseStatus.Enable, dbArticle.Status);
            Assert.AreEqual(2, dbArticle.CategoryId);
        }
        #endregion

        #region post: api/article
        [TestMethod]
        public async Task PostAsync()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 1, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);
            var controller = new ArticleController(svc, null);

            var dto = new PostArticleDto(db) { Title = "title", Content = "content", CategoryId = 1, Status = BaseStatus.Enable };
            await controller.Post(dto);

            var list = db.Articles.ToList();
            Assert.AreEqual(1, list.Count);

            var article = list.FirstOrDefault();
            Assert.AreEqual("title", article.Title);
            Assert.AreEqual("content", article.Content);
            Assert.AreEqual(1, article.CategoryId);
            Assert.AreEqual(BaseStatus.Enable, article.Status);
            Assert.AreEqual(1, await db.Articles.CountAsync());
        }
        #endregion

        #region Search
        [TestMethod]
        public async Task EmptySearchTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var result = await controller.Search(" ");

            Assert.AreEqual(StatusCodes.Status400BadRequest, controller.Response.StatusCode);
            Assert.AreEqual(NoticeMode.Info, result.NoticeMode);
            Assert.AreEqual("请输入关键词", result.Message);
        }

        [TestMethod]
        public async Task NetSearchWithoutOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var identity = new ClaimsIdentity(new List<Claim>());
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("net:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task NetSearchWithOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("net:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }

        [TestMethod]
        public async Task DatabaseSearchWithoutOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var identity = new ClaimsIdentity(new List<Claim>());
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("db:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task DatabaseSearchWithOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("db:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }

        [TestMethod]
        public async Task WindowsSearchWithoutOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var identity = new ClaimsIdentity(new List<Claim>());
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("windows:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task WindowsSearchWithOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("windows:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }

        [TestMethod]
        public async Task ReadSearchWithoutOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var identity = new ClaimsIdentity(new List<Claim>());
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("read:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task ReadSearchWithOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("read:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }

        [TestMethod]
        public async Task EssaysSearchWithoutOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var identity = new ClaimsIdentity(new List<Claim>());
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("essays:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task EssaysSearchWithOwnerTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(articleService, Mapper)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Search("essays:est");
            var data = result as HttpDataResult<Pagination<ArticleSummaryDto>>;
            var model = data.Data.Data.ToList();

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }
        #endregion
    }
}
