using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Test.Common;
using HappyDog.WebUI.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class CommentsViewComponentTest : TestBase
    {
        [TestMethod]
        public async Task GetEnabledCommentsTest()
        {
            await DbContext.Comments.AddAsync(new Comment
            {
                Id = 1,
                ArticleId = 1,
                Content = "`comment1`",
                CreateTime = new DateTimeOffset(new DateTime(2019, 11, 18), TimeSpan.FromHours(8)),
                Email = "hero_wong@outlook.com",
                Name = "test1",
                IPv4 = "localhost",
                Status = BaseStatus.Enabled
            });
            await DbContext.Comments.AddAsync(new Comment
            {
                Id = 2,
                ArticleId = 1,
                Content = "comment2",
                CreateTime = new DateTimeOffset(new DateTime(2019, 11, 19), TimeSpan.FromHours(8)),
                Email = "hero_wong@outlook.com",
                Name = "test2",
                IPv4 = "localhost:82",
                Status = BaseStatus.Disabled
            });
            await DbContext.Comments.AddAsync(new Comment
            {
                Id = 3,
                ArticleId = 1,
                Content = "**comment3**",
                CreateTime = new DateTimeOffset(new DateTime(2019, 11, 20), TimeSpan.FromHours(8)),
                Email = "hero_wong@outlook.com",
                Name = "test3",
                IPv4 = "localhost:83",
                Status = BaseStatus.Enabled
            });
            await DbContext.Comments.AddAsync(new Comment
            {
                Id = 4,
                ArticleId = 2,
                Content = "comment4",
                CreateTime = new DateTimeOffset(new DateTime(2019, 11, 20), TimeSpan.FromHours(8)),
                Email = "hero_wong@outlook.com",
                Name = "tes4",
                IPv4 = "localhost:84",
                Status = BaseStatus.Enabled
            });
            await DbContext.SaveChangesAsync();
            var svc = new CommentService(DbContext, Mapper, null);
            var vc = new CommentsViewComponent(svc);

            var result = await vc.InvokeAsync(1) as ViewViewComponentResult;
            var model = result.ViewData.Model as List<CommentDto>;
            Assert.AreEqual(2, model.Count);

            Assert.AreEqual("test3", model[1].Name);
            Assert.IsTrue(model[1].Content.StartsWith("<p><strong>comment3</strong></p>"));
            Assert.AreEqual(new DateTimeOffset(new DateTime(2019, 11, 20), TimeSpan.FromHours(8)), model[1].CreateTime);

            Assert.AreEqual("test1", model[0].Name);
            Assert.IsTrue(model[0].Content.StartsWith("<p><code>comment1</code></p>"));
            Assert.AreEqual(new DateTimeOffset(new DateTime(2019, 11, 18), TimeSpan.FromHours(8)), model[0].CreateTime);
        }
    }
}
