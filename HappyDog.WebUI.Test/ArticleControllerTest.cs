using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using HappyDog.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class ArticleControllerTest : TestBase
    {
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
            var svc = new ArticleService(db, Mapper);

            var controller = new ArticleController(svc, null)
            {
                PageSize = 2
            };

            var result = (await controller.Index()) as ViewResult;
            var model = result.Model as Pagination<ArticleDto>;

            Assert.AreEqual(2, model.Data.Count);
            Assert.AreEqual(8, model.Data[0].Id);
            Assert.AreEqual(7, model.TotalItems);
            Assert.AreEqual(4, model.TotalPages);
        }

        [TestMethod]
        public async Task DetailTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Category = new Category(), Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Category = new Category(), Content = "# H1", Status = BaseStatus.Enable });
            await db.SaveChangesAsync();

            var svc = new ArticleService(db, Mapper);
            var controller = new ArticleController(svc, null);

            var result = (await controller.Detail(2)) as ViewResult;
            var dto = result.Model as ArticleDetailDto;
            Assert.AreEqual(2, dto.Id);
        }

        [TestMethod]
        public async Task DetailNoFoundTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Content = "content1", Category = new Category(), Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Content = "content2", Category = new Category(), Status = BaseStatus.Enable });
            await db.SaveChangesAsync();

            var svc = new ArticleService(db, Mapper);
            var controller = new ArticleController(svc, null);

            var result = (await controller.Detail(1)) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

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
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Search(" ");
            var viewResult = result as ViewResult;
            var model = viewResult.Model as List<ArticleDto>;

            Assert.AreEqual(viewResult.ViewName, "EmptySearch");
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public async Task NetSearchTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var identity = new ClaimsIdentity(new List<Claim>());
            var controller = new ArticleController(articleService, null)
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
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task DatabaseSearchTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Search("db:est");
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task WindowsSearchTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Search("windows:est");
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task ReadSearchTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Search("read:est");
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task EssaysSearchTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Search("essays:est");
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task NetTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Net();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task DatabaseTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 2 });
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Database();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task WindowsTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 3 });
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Windows();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task ReadTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 4 });
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Read();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }

        [TestMethod]
        public async Task EssaysTest()
        {
            var db = new HappyDogContext(GetOptions());
            await db.Categories.AddAsync(new Category { Id = 5 });
            await db.Categories.AddAsync(new Category { Id = 1 });
            await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Disable });
            await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
            await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
            await db.SaveChangesAsync();
            var articleService = new ArticleService(db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Essays();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }
    }
}
