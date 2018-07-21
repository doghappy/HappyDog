using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using HappyDog.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class ArticleControllerTest : TestBase
    {
        #region Index
        [TestMethod]
        public async Task IndexNoAuthTest()
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
            var controller = new ArticleController(Mapper, svc, null)
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

        [TestMethod]
        public async Task IndexTest()
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


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(Mapper, svc, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                },
                PageSize = 2
            };

            var result = (await controller.Index(4)) as ViewResult;
            var model = result.Model as List<Article>;
            var pager = result.ViewData["Pager"] as Pager;

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(1, model[1].Id);
            Assert.AreEqual(BaseStatus.Disable, model[1].Status);
            Assert.AreEqual(8, pager.TotalItems);
            Assert.AreEqual(4, pager.TotalPages);
        }
        #endregion

        #region Detail
        [TestMethod]
        public async Task DetailNoAuthGetDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Category = new Category(), Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Category = new Category(), Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(false);
            var controller = new ArticleController(Mapper, svc, null)
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
        public async Task DetailNoAuthGetEnableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Category = new Category(), Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Category = new Category(), Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.IsAuthenticated).Returns(false);
            var controller = new ArticleController(Mapper, svc, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = mockPrincipal.Object
                    }
                }
            };

            var result = (await controller.Detail(2)) as ViewResult;
            var model = result.Model as Article;
            Assert.AreEqual(2, model.Id);
        }

        [TestMethod]
        public async Task DetailDisableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", CategoryId = 1, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", CategoryId = 1, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(Mapper, svc, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = (await controller.Detail(1)) as ViewResult;
            var model = result.Model as Article;
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual(BaseStatus.Disable, model.Status);
        }

        [TestMethod]
        public async Task DetailEnableTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Category = new Category(), Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Category = new Category(), Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var svc = new ArticleService(db);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Owner"),
            };
            var identity = new ClaimsIdentity(claims);
            var controller = new ArticleController(Mapper, svc, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = (await controller.Detail(2)) as ViewResult;
            var model = result.Model as Article;
            Assert.AreEqual(2, model.Id);
        }
        #endregion

        [TestMethod]
        public async Task PutTest()
        {
            var db = new HappyDogContext(GetOptions());
            var article = new Article { Id = 1, Title = "title1", Content = "content1", CategoryId = 1, Status = BaseStatus.Disable };
            await db.AddAsync(article);
            await db.SaveChangesAsync();

            var svc = new ArticleService(db);
            var controller = new ArticleController(Mapper, svc, null);

            var dto = new EditArticleDto { Title = "title2", Content = "content2", CategoryId = 2, State = BaseStatus.Enable };
            await controller.Edit(1, dto);

            Assert.AreEqual(1, article.Id);
            Assert.AreEqual("title2", article.Title);
            Assert.AreEqual("content2", article.Content);
            Assert.AreEqual(BaseStatus.Enable, article.Status);
            Assert.AreEqual(2, article.CategoryId);
        }

        [TestMethod]
        public async Task PostTest()
        {
            var db = new HappyDogContext(GetOptions());
            var svc = new ArticleService(db);
            var controller = new ArticleController(Mapper, svc, null);

            var dto = new PostArticleDto { Title = "title", Content = "content", CategoryId = 1, State = BaseStatus.Enable };
            await controller.Post(dto);

            var list = await db.Articles.ToListAsync();
            Assert.AreEqual(1, list.Count);

            var article = list.FirstOrDefault();
            Assert.AreEqual("title", article.Title);
            Assert.AreEqual("content", article.Content);
            Assert.AreEqual(1, article.CategoryId);
            Assert.AreEqual(BaseStatus.Enable, article.Status);
        }

        #region Search
        [TestMethod]
        public async Task EmptySearchTest()
        {
            var controller = new ArticleController(null, null, null);
            var result = await controller.Search(" ");
            var viewResult = result as ViewResult;

            Assert.AreEqual(viewResult.ViewName, "EmptySearch");
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
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
            var controller = new ArticleController(null, articleService, null)
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
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }
        #endregion

        #region Net
        [TestMethod]
        public async Task NetWithoutOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Net();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task NetWithOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Net();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }
        #endregion

        #region Database
        [TestMethod]
        public async Task DatabaseWithoutOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Database();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task DatabaseWithOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Database();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }
        #endregion

        #region Windows
        [TestMethod]
        public async Task WindowsWithoutOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Windows();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task WindowsWithOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Windows();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }
        #endregion

        #region Read
        [TestMethod]
        public async Task ReadWithoutOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Read();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task ReadWithOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Read();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }
        #endregion

        #region Essays
        [TestMethod]
        public async Task EssaysWithoutOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Essays();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(2, model[0].Id);
        }

        [TestMethod]
        public async Task EssaysWithOwnerTest()
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
            var controller = new ArticleController(null, articleService, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(identity)
                    }
                }
            };

            var result = await controller.Essays();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as List<Article>;
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(2, model[0].Id);
            Assert.AreEqual(1, model[1].Id);
        }
        #endregion
    }
}
