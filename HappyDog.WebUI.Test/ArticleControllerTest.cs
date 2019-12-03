using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.IServices;
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
        HappyDogContext _db;

        [TestInitialize]
        public void Initialize()
        {
            _db = new HappyDogContext(GetOptions());
        }

        [TestCleanup]
        public async Task CleanupAsync()
        {
            await _db.DisposeAsync();
        }

        [TestMethod]
        public async Task DetailTest()
        {
            var mockSvc = new Mock<IArticleService>();
            mockSvc
                .Setup(s => s.GetEnabledArticleDetailDtoAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(new ArticleDetailDto
                {
                    Title = "article 1",
                    CategoryId = ArticleCategory.Net,
                    Content = "**Content**"
                }));
            using var controller = new ArticleController(mockSvc.Object, null);
            var result = (await controller.Detail(It.IsAny<int>())) as ViewResult;
            var dto = result.Model as ArticleDetailDto;
            Assert.AreEqual("article 1", dto.Title);
            Assert.AreEqual(ArticleCategory.Net, dto.CategoryId);
            Assert.IsTrue(dto.Content.StartsWith("<p><strong>Content</strong></p>"));
            mockSvc.Verify(m => m.GetEnabledArticleDetailDtoAsync(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public async Task DetailNoFoundTest()
        {
            var mockSvc = new Mock<IArticleService>();
            mockSvc
                .Setup(s => s.GetArticleDetailDtoAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(default(ArticleDetailDto)));
            using var controller = new ArticleController(mockSvc.Object, null);
            var result = (await controller.Detail(1)) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task EmptySearchTest()
        {
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Essays });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Net });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Net, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Net });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Database });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Net, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Net, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Database, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Database });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Windows });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Database, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Database, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Windows, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Windows });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Read });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Windows, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Windows, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Read, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Read });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Essays });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Read, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Read, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Essays });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Net });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Net, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Net });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Database });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Net, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Net, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Database, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Database });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Windows });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Database, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Database, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Windows, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Windows });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Read });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Windows, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Windows, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Read, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Read });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Essays });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Read, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Read, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
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
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Essays });
            await _db.Categories.AddAsync(new Category { Id = ArticleCategory.Net });
            await _db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Disabled });
            await _db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = ArticleCategory.Essays, Status = BaseStatus.Enabled });
            await _db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = ArticleCategory.Net, Status = BaseStatus.Enabled });
            await _db.SaveChangesAsync();
            var articleService = new ArticleService(_db, Mapper);
            var controller = new ArticleController(articleService, null);

            var result = await controller.Essays();
            var viewReuslt = result as ViewResult;
            var model = viewReuslt.Model as Pagination<ArticleDto>;
            Assert.AreEqual(1, model.Data.Count);
            Assert.AreEqual(2, model.Data[0].Id);
        }
    }
}
