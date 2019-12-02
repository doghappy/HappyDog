using HappyDog.Console.Api.Controllers;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.IServices;
using HappyDog.Test.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HappyDog.Console.Api.Test
{
    [TestClass]
    public class ArticleControllerTest : TestBase
    {
        [TestMethod]
        public async Task DisabledListTest()
        {
            var mockSvc = new Mock<IArticleService>();
            mockSvc
                .Setup(m => m.GetDisabledArticlesDtoAsync())
                .Returns(Task.FromResult(new List<ArticleDto>
                {
                    new ArticleDto { Id = 1, Title = "ArticleDto 1" },
                    new ArticleDto { Id = 2, Title = "ArticleDto 2" }
                }));

            var controller = new ArticleController(mockSvc.Object);
            var list = await controller.GetDisabledList();

            Assert.AreEqual(2, list.Count);

            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual("ArticleDto 1", list[0].Title);

            Assert.AreEqual(2, list[1].Id);
            Assert.AreEqual("ArticleDto 2", list[1].Title);
        }

        [TestMethod]
        public async Task Put_not_exists_article()
        {
            var mockSvc = new Mock<IArticleService>();
            mockSvc
                .Setup(m => m.PutAsync(It.IsAny<int>(), It.IsAny<PutArticleDto>()))
                .Returns(Task.FromResult(default(ArticleDetailDto)));

            var controller = new ArticleController(mockSvc.Object);
            var actionResult = await controller.Put(1, new PutArticleDto());
            var notFoundResult = actionResult as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [TestMethod]
        public async Task Put_exists_article()
        {
            var mockSvc = new Mock<IArticleService>();
            mockSvc
                .Setup(m => m.PutAsync(It.IsAny<int>(), It.IsAny<PutArticleDto>()))
                .Returns(Task.FromResult(new ArticleDetailDto
                {
                    Id = 666,
                    Title = "test111"
                }));

            var controller = new ArticleController(mockSvc.Object);
            var actionResult = await controller.Put(1, new PutArticleDto());
            var jsonResult = actionResult as JsonResult;
            var dto = jsonResult.Value as ArticleDetailDto;

            Assert.AreEqual(666, dto.Id);
            Assert.AreEqual("test111", dto.Title);
        }
    }
}
