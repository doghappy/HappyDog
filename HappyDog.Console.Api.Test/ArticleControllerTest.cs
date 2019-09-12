using HappyDog.Console.Api.Controllers;
using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HappyDog.Console.Api.Test
{
    [TestClass]
    public class ArticleControllerTest : TestBase
    {
        [TestMethod]
        public async Task DisabledListTest()
        {
            using (var db = new HappyDogContext(GetOptions()))
            {
                await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
                await db.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enable, Category = new Category() });
                await db.SaveChangesAsync();

                var controller = new ArticleController(db, Mapper);
                var data = await controller.DisabledList();

                Assert.AreEqual(1, data.Count);
                Assert.AreEqual("article 1", data[0].Title);
            }
        }

        [TestMethod]
        public async Task PostTest()
        {
            using (var db = new HappyDogContext(GetOptions()))
            {
                var controller = new ArticleController(db, Mapper);
                var postDto = new PostArticleDto
                {
                    Title = "Title",
                    CategoryId = 1,
                    Content = "Content",
                    Status = BaseStatus.Enable
                };

                var detailDto = await controller.Post(postDto);
                var articles = await db.Articles.ToListAsync();

                Assert.AreEqual("Title", detailDto.Title);
                Assert.AreEqual("Content", detailDto.Content);
                Assert.AreEqual(1, detailDto.CategoryId);
                Assert.AreEqual(BaseStatus.Enable, detailDto.Status);

                Assert.AreEqual(1, articles.Count);
                Assert.AreEqual(1, articles[0].Id);
                Assert.AreEqual("Title", articles[0].Title);
                Assert.AreEqual("Content", articles[0].Content);
                Assert.AreEqual(1, articles[0].CategoryId);
                Assert.AreEqual(BaseStatus.Enable, articles[0].Status);
            }
        }

        [TestMethod]
        public async Task PutNotFoundTest()
        {
            using (var db = new HappyDogContext(GetOptions()))
            {
                var controller = new ArticleController(db, Mapper);
                var putDto = new PutArticleDto
                {
                    Title = "Title",
                    CategoryId = 1,
                    Content = "Content",
                    Status = BaseStatus.Enable
                };

                var notFoundResult = await controller.Put(1, putDto) as NotFoundResult;
                var articles = await db.Articles.ToListAsync();

                Assert.AreEqual(404, notFoundResult.StatusCode);
                Assert.AreEqual(0, articles.Count);
            }
        }

        [TestMethod]
        public async Task PutTest()
        {
            using (var db = new HappyDogContext(GetOptions()))
            {
                await db.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disable, Category = new Category() });
                await db.SaveChangesAsync();

                var controller = new ArticleController(db, Mapper);
                var putDto = new PutArticleDto
                {
                    Title = "Title",
                    CategoryId = 1,
                    Content = "Content",
                    Status = BaseStatus.Enable
                };

                var jsonResult = await controller.Put(1, putDto) as JsonResult;
                var detailDto = jsonResult.Value as ArticleDetailDto;
                var articles = await db.Articles.ToListAsync();

                Assert.AreEqual("Title", detailDto.Title);
                Assert.AreEqual("Content", detailDto.Content);
                Assert.AreEqual(1, detailDto.CategoryId);
                Assert.AreEqual(BaseStatus.Enable, detailDto.Status);

                Assert.AreEqual(1, articles.Count);
                Assert.AreEqual(1, articles[0].Id);
                Assert.AreEqual("Title", articles[0].Title);
                Assert.AreEqual("Content", articles[0].Content);
                Assert.AreEqual(1, articles[0].CategoryId);
                Assert.AreEqual(BaseStatus.Enable, articles[0].Status);
            }
        }
    }
}
