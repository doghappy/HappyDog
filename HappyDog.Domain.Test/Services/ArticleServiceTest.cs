using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Test.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HappyDog.Domain.Test.Services
{
    [TestClass]
    public class ArticleServiceTest : TestBase
    {
        [TestMethod]
        [Description("获取单个公开的Article详情")]
        public async Task Get_enabled_article_detail()
        {
            var now = DateTimeOffset.Now;
            await DbContext.Articles.AddAsync(new Article
            {
                Title = "article",
                Content = "# H1",
                ViewCount = 165,
                CreateTime = now,
                Status = BaseStatus.Enabled,
                Category = new Category
                {
                    Id = ArticleCategory.Database,
                    Color = "",
                    Label = "",
                    Value = ""
                },
                ArticleTags = new List<ArticleTag>
                {
                    new ArticleTag { Tag = new Tag { Name = "t1" } },
                    new ArticleTag { Tag = new Tag { Name = "t2" } }
                }
            });
            await DbContext.SaveChangesAsync();

            var svc = new ArticleService(DbContext, Mapper);
            var dto = await svc.GetEnabledArticleDetailDtoAsync(1);

            Assert.AreEqual(1, dto.Id);
            Assert.AreEqual(ArticleCategory.Database, dto.CategoryId);
            Assert.AreEqual("article", dto.Title);
            Assert.AreEqual("# H1", dto.Content);
            Assert.AreEqual(BaseStatus.Enabled, dto.Status);
            Assert.AreEqual(166, dto.ViewCount);
            Assert.AreEqual(now, dto.CreateTime);
            Assert.AreEqual(2, dto.Tags.Count);
            Assert.AreEqual("t1", dto.Tags[0].Name);
            Assert.AreEqual("t2", dto.Tags[1].Name);
        }

        [TestMethod]
        [Description("获取单个Article详情")]
        public async Task Get_an_article_detail()
        {
            var now = DateTimeOffset.Now;
            await DbContext.Articles.AddAsync(new Article
            {
                Title = "article",
                Content = "# H1",
                ViewCount = 165,
                CreateTime = now,
                Status = BaseStatus.Disabled,
                Category = new Category
                {
                    Id = ArticleCategory.Database,
                    Color = "",
                    Label = "",
                    Value = ""
                },
                ArticleTags = new List<ArticleTag>
                {
                    new ArticleTag { Tag = new Tag { Name = "t1" } },
                    new ArticleTag { Tag = new Tag { Name = "t2" } }
                }
            });
            await DbContext.SaveChangesAsync();

            var svc = new ArticleService(DbContext, Mapper);
            var dto = await svc.GetArticleDetailDtoAsync(1);

            Assert.AreEqual(1, dto.Id);
            Assert.AreEqual(ArticleCategory.Database, dto.CategoryId);
            Assert.AreEqual("article", dto.Title);
            Assert.AreEqual("# H1", dto.Content);
            Assert.AreEqual(BaseStatus.Disabled, dto.Status);
            Assert.AreEqual(165, dto.ViewCount);
            Assert.AreEqual(now, dto.CreateTime);
            Assert.AreEqual(2, dto.Tags.Count);
            Assert.AreEqual("t1", dto.Tags[0].Name);
            Assert.AreEqual("t2", dto.Tags[1].Name);
        }

        [TestMethod]
        [Description("获取不存在的Article详情")]
        public async Task Get_not_exists_article_detail()
        {
            var svc = new ArticleService(DbContext, Mapper);
            var dto = await svc.GetArticleDetailDtoAsync(1);
            Assert.IsNull(dto);
        }

        [TestMethod]
        [Description("分页获取文章列表")]
        public async Task Get_pagination_article_list()
        {
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Net, Color = "", Label = "", Value = "" });
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Database, Color = "", Label = "", Value = "" });
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Windows, Color = "", Label = "", Value = "" });
            await DbContext.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disabled, CategoryId = ArticleCategory.Net });
            await DbContext.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Windows });
            await DbContext.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Net });
            await DbContext.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Windows });
            await DbContext.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.SaveChangesAsync();
            var svc = new ArticleService(DbContext, Mapper);

            var result = await svc.GetArticlesDtoAsync(1, 2, null);

            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual(8, result.Data[0].Id);
            Assert.AreEqual(7, result.TotalItems);
            Assert.AreEqual(4, result.TotalPages);
        }

        [TestMethod]
        [Description("分页获取Database文章列表")]
        public async Task Get_pagination_database_list()
        {
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Database, Color = "", Label = "", Value = "" });
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Windows, Color = "", Label = "", Value = "" });
            await DbContext.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Windows });
            await DbContext.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Net });
            await DbContext.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Windows });
            await DbContext.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.SaveChangesAsync();
            var svc = new ArticleService(DbContext, Mapper);

            var result = await svc.GetArticlesDtoAsync(1, 2, ArticleCategory.Database);

            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual(8, result.Data[0].Id);
            Assert.AreEqual("article 8", result.Data[0].Title);
            Assert.AreEqual(4, result.TotalItems);
            Assert.AreEqual(2, result.TotalPages);
        }

        [TestMethod]
        [Description("获取被禁用的文章")]
        public async Task Get_disabled_articles()
        {
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Database, Color = "", Label = "", Value = "" });
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Windows, Color = "", Label = "", Value = "" });
            await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Net, Color = "", Label = "", Value = "" });
            await DbContext.Articles.AddAsync(new Article { Id = 1, Title = "article 1", Status = BaseStatus.Disabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 2, Title = "article 2", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 3, Title = "article 3", Status = BaseStatus.Disabled, CategoryId = ArticleCategory.Windows });
            await DbContext.Articles.AddAsync(new Article { Id = 4, Title = "article 4", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 5, Title = "article 5", Status = BaseStatus.Disabled, CategoryId = ArticleCategory.Net });
            await DbContext.Articles.AddAsync(new Article { Id = 6, Title = "article 6", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.Articles.AddAsync(new Article { Id = 7, Title = "article 7", Status = BaseStatus.Disabled, CategoryId = ArticleCategory.Windows });
            await DbContext.Articles.AddAsync(new Article { Id = 8, Title = "article 8", Status = BaseStatus.Enabled, CategoryId = ArticleCategory.Database });
            await DbContext.SaveChangesAsync();
            var svc = new ArticleService(DbContext, Mapper);

            var result = await svc.GetDisabledArticlesDtoAsync();

            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(1, result[3].Id);
            Assert.AreEqual("article 1", result[3].Title);
            Assert.AreEqual(ArticleCategory.Database, result[3].CategoryId);

            Assert.AreEqual(7, result[0].Id);
            Assert.AreEqual("article 7", result[0].Title);
            Assert.AreEqual(ArticleCategory.Windows, result[0].CategoryId);
        }

        [TestMethod]
        [Description("新建文章")]
        public async Task Post()
        {
            await DbContext.Tags.AddAsync(new Tag { Name = ".net" });
            await DbContext.SaveChangesAsync();
            var svc = new ArticleService(DbContext, Mapper);

            var postDto = new PostArticleDto
            {
                Title = Guid.NewGuid().ToString(),
                CategoryId = ArticleCategory.Database,
                Content = Guid.NewGuid().ToString(),
                Status = BaseStatus.Disabled,
                TagIds = new List<int> { 1, 2, 1 }
            };

            await svc.PostAsync(postDto);
            var articles = await DbContext.Articles
                .AsNoTracking()
                .Include(a => a.ArticleTags)
                .ThenInclude(at => at.Tag)
                .ToListAsync();
            var totalTags = await DbContext.Tags.CountAsync();

            Assert.AreEqual(1, articles.Count);
            Assert.AreEqual(1, articles[0].Id);
            Assert.AreEqual(postDto.Title, articles[0].Title);
            Assert.AreEqual(postDto.CategoryId, articles[0].CategoryId);
            Assert.AreEqual(postDto.Content, articles[0].Content);
            Assert.AreEqual(postDto.Status, articles[0].Status);
            Assert.AreEqual(1, articles[0].ArticleTags.Count);
            Assert.AreEqual(".net", articles[0].ArticleTags[0].Tag.Name);
            Assert.AreEqual(1, totalTags);
        }

        [TestMethod]
        [Description("修改不存在的文章")]
        public async Task Put_not_exists_article()
        {
            var svc = new ArticleService(DbContext, Mapper);

            var putDto = new PutArticleDto
            {
                Title = Guid.NewGuid().ToString(),
                CategoryId = ArticleCategory.Database,
                Content = Guid.NewGuid().ToString(),
                Status = BaseStatus.Disabled
            };
            var result = await svc.PutAsync(1, putDto);

            Assert.IsNull(result);
        }

        [TestMethod]
        [Description("修改文章")]
        public async Task Put_article()
        {
            await DbContext.Tags.AddAsync(new Tag { Name = "css" });
            await DbContext.Tags.AddAsync(new Tag { Name = "js" });
            await DbContext.Tags.AddAsync(new Tag { Name = "UWP" });
            await DbContext.Articles.AddAsync(new Article
            {
                Id = 1,
                Title = "article 1",
                Status = BaseStatus.Enabled,
                CategoryId = ArticleCategory.Net,
                ArticleTags = new List<ArticleTag>
                {
                    new ArticleTag { TagId = 1 },
                    new ArticleTag { TagId = 2 }
                }
            });
            await DbContext.SaveChangesAsync();
            var svc = new ArticleService(DbContext, Mapper);

            var putDto = new PutArticleDto
            {
                Title = Guid.NewGuid().ToString(),
                CategoryId = ArticleCategory.Database,
                Content = Guid.NewGuid().ToString(),
                Status = BaseStatus.Disabled,
                TagIds = new List<int> { 5, 3, 4 }
            };
            await svc.PutAsync(1, putDto);

            var articles = await DbContext.Articles
                .AsNoTracking()
                .Include(a => a.ArticleTags)
                .ThenInclude(t => t.Tag)
                .ToListAsync();
            var tagsCount = await DbContext.Tags.CountAsync();

            Assert.AreEqual(1, articles.Count);
            Assert.AreEqual(putDto.Title, articles[0].Title);
            Assert.AreEqual(putDto.CategoryId, articles[0].CategoryId);
            Assert.AreEqual(putDto.Content, articles[0].Content);
            Assert.AreEqual(putDto.Status, articles[0].Status);
            Assert.AreEqual(1, articles[0].ArticleTags.Count);
            Assert.AreEqual("UWP", articles[0].ArticleTags[0].Tag.Name);
            Assert.AreEqual(3, tagsCount);
        }
    }
}
