using HappyDog.Domain.DataTransferObjects.Comment;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Test.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace HappyDog.Domain.Test.Services
{
    [TestClass]
    public class CommentServiceTest : TestBase
    {
        [TestMethod]
        [Description("发送评论")]
        public async Task Post_comments()
        {
            var svc = new CommentService(DbContext, Mapper);
            var dto = new PostCommentDto
            {
                Content = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                ArticleId = 1,
                Code = "1234",
                Email = "hero_wong@outlook.com",
                IPv4 = "localhost:123"
            };
            await svc.CreateAsync(dto);
            var result = await DbContext.Comments.FirstOrDefaultAsync();
            Assert.AreEqual(dto.Content, result.Content);
            Assert.AreEqual(dto.ArticleId, result.ArticleId);
            Assert.AreEqual(dto.Name, result.Name);
            Assert.AreEqual(DateTimeOffset.Now.ToString("yyyy-MM-dd"), result.CreateTime.ToString("yyyy-MM-dd"));
            Assert.AreEqual(dto.Email, result.Email);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("localhost:123", result.IPv4);
            Assert.AreEqual(BaseStatus.Enabled, result.Status);
        }

        [TestMethod]
        [Description("当文章没有评论时获取所有评论")]
        public async Task Get_comments_when_article_has_no_comments()
        {
            var svc = new CommentService(DbContext, Mapper);
            var comments = await svc.GetCommentDtosAsync(1);
            Assert.AreEqual(0, comments.Count);
        }

        [TestMethod]
        [Description("当文章有评论时获取所有评论")]
        public async Task Get_comments_when_article_has_comments()
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

            var svc = new CommentService(DbContext, Mapper);
            var comments = await svc.GetCommentDtosAsync(1);
            Assert.AreEqual(2, comments.Count);

            Assert.AreEqual("test3", comments[1].Name);
            Assert.AreEqual("**comment3**", comments[1].Content);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2019, 11, 20), TimeSpan.FromHours(8)), comments[1].CreateTime);

            Assert.AreEqual("test1", comments[0].Name);
            Assert.AreEqual("`comment1`", comments[0].Content);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2019, 11, 18), TimeSpan.FromHours(8)), comments[0].CreateTime);
        }
    }
}
