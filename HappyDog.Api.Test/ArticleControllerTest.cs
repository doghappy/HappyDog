using HappyDog.Api.Controllers;
using HappyDog.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using HappyDog.Infrastructure;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.IServices;
using Moq;

namespace HappyDog.Api.Test
{
    [TestClass]
    public class ArticleControllerTest
    {
        //需要将搜索的测试转移到Domain.Test .

        [TestMethod]
        public async Task Search_with_empty_key()
        {
            var mockSvc = new Mock<IArticleService>();
            using var articleContoller = new ArticleController(mockSvc.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            var result = await articleContoller.Search(null);

            Assert.AreEqual("请输入关键词", result.Message);
            Assert.AreEqual(NoticeMode.Info, result.NoticeMode);
            mockSvc.Verify(m => m.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public async Task Search_with_key()
        {
            var mockSvc = new Mock<IArticleService>();
            mockSvc
                .Setup(m => m.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new Pagination<ArticleDto>()));
            using var articleContoller = new ArticleController(mockSvc.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            var result = await articleContoller.Search("aa");

            mockSvc.Verify(m => m.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        //[TestMethod]
        //public async Task GetTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc);

        //    var result = (await controller.Detail(2)) as JsonResult;
        //    var data = result.Value as ArticleDto;
        //    Assert.IsNotNull(data);
        //    Assert.AreEqual(2, data.Id);
        //    Assert.AreEqual(1, data.ViewCount);
        //}

        //[TestMethod]
        //public async Task NotFoundTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc);

        //    var result = (await controller.Detail(3)) as NotFoundResult;
        //    Assert.AreEqual(404, result.StatusCode);
        //}

        //[TestMethod]
        //public async Task GetListWithDefaultPageIndex()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2
        //    };

        //    var result = await controller.List();
        //    Assert.AreEqual(7, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(2, data.Count);
        //    Assert.AreEqual(8, data[0].Id);
        //}

        //[TestMethod]
        //public async Task GetListWithPageIndexEq4()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2,
        //    };

        //    var result = await controller.List(4);
        //    Assert.AreEqual(7, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(1, data.Count);
        //    Assert.AreEqual(2, data[0].Id);
        //}

        //[TestMethod]
        //public async Task GetListWithOutRangePageIndex()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enable, Category = new Category() });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2
        //    };

        //    var result = await controller.List(5);
        //    Assert.AreEqual(7, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(0, data.Count);
        //}

        //[TestMethod]
        //public async Task GetNetTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    var c1 = new Category { Id = 1 };
        //    var c2 = new Category { Id = 2 };
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c1 });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c2 });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c1 });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c1 });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2
        //    };

        //    var result = await controller.Net();
        //    Assert.AreEqual(2, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(2, data.Count);
        //    Assert.AreEqual(4, data[0].Id);
        //}

        //[TestMethod]
        //public async Task GetDatabaseTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    var c1 = new Category { Id = 1 };
        //    var c2 = new Category { Id = 2 };
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c2 });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c2 });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c2 });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2
        //    };

        //    var result = await controller.Database();
        //    Assert.AreEqual(2, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(2, data.Count);
        //    Assert.AreEqual(4, data[0].Id);
        //}

        //[TestMethod]
        //public async Task GetWindowsTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    var c1 = new Category { Id = 1 };
        //    var c3 = new Category { Id = 3 };
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c3 });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c3 });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c3 });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2
        //    };

        //    var result = await controller.Windows();
        //    Assert.AreEqual(2, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(2, data.Count);
        //    Assert.AreEqual(4, data[0].Id);
        //}

        //[TestMethod]
        //public async Task GetReadTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    var c1 = new Category { Id = 1 };
        //    var c4 = new Category { Id = 4 };
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c4 });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c4 });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c4 });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2
        //    };

        //    var result = await controller.Read();
        //    Assert.AreEqual(2, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(2, data.Count);
        //    Assert.AreEqual(4, data[0].Id);
        //}

        //[TestMethod]
        //public async Task GetEssaysTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    var c1 = new Category { Id = 1 };
        //    var c5 = new Category { Id = 5 };
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = c5 });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = c1 });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enable, Category = c5 });
        //    await db.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enable, Category = c5 });
        //    await db.SaveChangesAsync();
        //    var svc = new ArticleService(db, Mapper);

        //    var controller = new ArticleController(svc)
        //    {
        //        PageSize = 2
        //    };

        //    var result = await controller.Essays();
        //    Assert.AreEqual(2, result.TotalItems);

        //    var data = result.Data.ToList();
        //    Assert.AreEqual(2, data.Count);
        //    Assert.AreEqual(4, data[0].Id);
        //}

        //[TestMethod]
        //public async Task EmptySearchTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Categories.AddAsync(new Category { Id = 5 });
        //    await db.Categories.AddAsync(new Category { Id = 1 });
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Disable });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
        //    await db.SaveChangesAsync();
        //    var articleService = new ArticleService(db, Mapper);
        //    var controller = new ArticleController(articleService)
        //    {
        //        ControllerContext = new ControllerContext
        //        {
        //            HttpContext = new DefaultHttpContext()
        //        }
        //    };

        //    var result = await controller.Search(" ");

        //    Assert.AreEqual(StatusCodes.Status400BadRequest, controller.Response.StatusCode);
        //    Assert.AreEqual(NoticeMode.Info, result.NoticeMode);
        //    Assert.AreEqual("请输入关键词", result.Message);
        //}

        //[TestMethod]
        //public async Task NetSearchTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Categories.AddAsync(new Category { Id = 1 });
        //    await db.Categories.AddAsync(new Category { Id = 2 });
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Disable });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
        //    await db.SaveChangesAsync();
        //    var articleService = new ArticleService(db, Mapper);
        //    var controller = new ArticleController(articleService);

        //    var result = await controller.Search("net:est");
        //    var data = result as HttpDataResult<Pagination<ArticleDto>>;
        //    var model = data.Data.Data.ToList();

        //    Assert.AreEqual(1, model.Count);
        //    Assert.AreEqual(2, model[0].Id);
        //}

        //[TestMethod]
        //public async Task DatabaseSearchTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Categories.AddAsync(new Category { Id = 2 });
        //    await db.Categories.AddAsync(new Category { Id = 3 });
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Disable });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Database, Status = BaseStatus.Enable });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
        //    await db.SaveChangesAsync();
        //    var articleService = new ArticleService(db, Mapper);
        //    var controller = new ArticleController(articleService);

        //    var result = await controller.Search("db:est");
        //    var data = result as HttpDataResult<Pagination<ArticleDto>>;
        //    var model = data.Data.Data.ToList();

        //    Assert.AreEqual(1, model.Count);
        //    Assert.AreEqual(2, model[0].Id);
        //}

        //[TestMethod]
        //public async Task WindowsSearchTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Categories.AddAsync(new Category { Id = 3 });
        //    await db.Categories.AddAsync(new Category { Id = 4 });
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Disable });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Windows, Status = BaseStatus.Enable });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
        //    await db.SaveChangesAsync();
        //    var articleService = new ArticleService(db, Mapper);
        //    var controller = new ArticleController(articleService);

        //    var result = await controller.Search("windows:est");
        //    var data = result as HttpDataResult<Pagination<ArticleDto>>;
        //    var model = data.Data.Data.ToList();

        //    Assert.AreEqual(1, model.Count);
        //    Assert.AreEqual(2, model[0].Id);
        //}

        //[TestMethod]
        //public async Task ReadSearchTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Categories.AddAsync(new Category { Id = 4 });
        //    await db.Categories.AddAsync(new Category { Id = 5 });
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Disable });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Read, Status = BaseStatus.Enable });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
        //    await db.SaveChangesAsync();
        //    var articleService = new ArticleService(db, Mapper);
        //    var controller = new ArticleController(articleService);

        //    var result = await controller.Search("read:est");
        //    var data = result as HttpDataResult<Pagination<ArticleDto>>;
        //    var model = data.Data.Data.ToList();

        //    Assert.AreEqual(1, model.Count);
        //    Assert.AreEqual(2, model[0].Id);
        //}

        //[TestMethod]
        //public async Task EssaysSearchTest()
        //{
        //    var db = new HappyDogContext(GetOptions());
        //    await db.Categories.AddAsync(new Category { Id = 5 });
        //    await db.Categories.AddAsync(new Category { Id = 1 });
        //    await db.Articles.AddAsync(new Article { Id = 1, Title = "test1", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Disable });
        //    await db.Articles.AddAsync(new Article { Id = 2, Title = "test2", CategoryId = (int)ArticleCategory.Essays, Status = BaseStatus.Enable });
        //    await db.Articles.AddAsync(new Article { Id = 3, Title = "test3", CategoryId = (int)ArticleCategory.Net, Status = BaseStatus.Enable });
        //    await db.SaveChangesAsync();
        //    var articleService = new ArticleService(db, Mapper);
        //    var controller = new ArticleController(articleService)
        //    {
        //        ControllerContext = new ControllerContext
        //        {
        //            HttpContext = new DefaultHttpContext()
        //        }
        //    };

        //    var result = await controller.Search("essays:est");
        //    var data = result as HttpDataResult<Pagination<ArticleDto>>;
        //    var model = data.Data.Data.ToList();

        //    Assert.AreEqual(1, model.Count);
        //    Assert.AreEqual(2, model[0].Id);
        //}
    }
}
