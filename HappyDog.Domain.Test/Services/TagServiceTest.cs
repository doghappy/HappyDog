using HappyDog.Domain.DataTransferObjects.Tag;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Test.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.Test.Services
{
    [TestClass]
    public class TagServiceTest : TestBase
    {
        [TestMethod]
        public async Task Post()
        {
            var svc = new TagService(DbContext, Mapper);
            var postDto = new PostTagDto
            {
                Name = "Visual Studio",
                Color = "Purple"
            };

            await svc.PostAsync(postDto);

            var tags = await DbContext.Tags.ToListAsync();

            Assert.AreEqual(1, tags.Count);
            Assert.AreEqual("Visual Studio", tags[0].Name);
            Assert.AreEqual("Purple", tags[0].Color);
            Assert.IsNull(tags[0].Glyph);
            Assert.IsNull(tags[0].GlyphFont);
        }

        [TestMethod]
        public async Task Get_some_tags_through_search()
        {
            await DbContext.Tags.AddAsync(new Tag { Name = "VS Code" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Linux" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Visual Studio" });
            await DbContext.SaveChangesAsync();

            var svc = new TagService(DbContext, Mapper);
            var result = await svc.SearchTagsDtoAsync("o");

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("VS Code", result[0].Name);
            Assert.AreEqual("Visual Studio", result[1].Name);
        }

        [TestMethod]
        public async Task No_tags_through_search()
        {
            await DbContext.Tags.AddAsync(new Tag { Name = "VS Code" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Linux" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Visual Studio" });
            await DbContext.SaveChangesAsync();

            var svc = new TagService(DbContext, Mapper);
            var result = await svc.SearchTagsDtoAsync("Net");

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task Get_all_tags()
        {
            await DbContext.Tags.AddAsync(new Tag { Name = "VS Code" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Linux" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Visual Studio" });
            await DbContext.SaveChangesAsync();

            var svc = new TagService(DbContext, Mapper);
            var result = await svc.SearchTagsDtoAsync(string.Empty);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("VS Code", result[0].Name);
            Assert.AreEqual("Linux", result[1].Name);
            Assert.AreEqual("Visual Studio", result[2].Name);
        }

        [TestMethod]
        public async Task Get_a_tag_by_id()
        {
            await DbContext.Tags.AddAsync(new Tag { Name = "VS Code" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Linux" });
            await DbContext.Tags.AddAsync(new Tag { Name = "Visual Studio" });
            await DbContext.SaveChangesAsync();

            var svc = new TagService(DbContext, Mapper);
            var result = await svc.GetTagDtoAsync(1);

            Assert.AreEqual("VS Code", result.Name);
        }

        [TestMethod]
        public async Task Put()
        {
            await DbContext.Tags.AddAsync(new Tag { Name = "Visual Studio" });
            await DbContext.SaveChangesAsync();

            var svc = new TagService(DbContext, Mapper);
            var putDto = new PutTagDto
            {
                Name = "VS",
                Color = "Blue",
                GlyphFont = "Segoe MDL2 Assets"
            };

            await svc.PutAsync(1, putDto);

            var tags = await DbContext.Tags.ToListAsync();

            Assert.AreEqual(1, tags.Count);
            Assert.AreEqual("VS", tags[0].Name);
            Assert.AreEqual("Blue", tags[0].Color);
            Assert.IsNull(tags[0].Glyph);
            Assert.AreEqual("Segoe MDL2 Assets", tags[0].GlyphFont);
        }

        [TestMethod]
        public async Task Get_articles_by_tag_name()
        {
            var c1 = await DbContext.Categories.AddAsync(new Category { Id = ArticleCategory.Net });
            var tag1 = await DbContext.Tags.AddAsync(new Tag { Name = "Tag1" });
            var tag2 = await DbContext.Tags.AddAsync(new Tag { Name = "Tag2" });
            await DbContext.Articles.AddAsync(new Article
            {
                Title = "article 1",
                Status = BaseStatus.Enabled,
                ArticleTags = new List<ArticleTag>
                {
                    new ArticleTag { Tag = tag1.Entity }
                },
                Category = c1.Entity
            });
            await DbContext.Articles.AddAsync(new Article
            {
                Title = "article 2",
                Status = BaseStatus.Enabled,
                ArticleTags = new List<ArticleTag>
                {
                    new ArticleTag { Tag = tag1.Entity },
                    new ArticleTag { Tag = tag2.Entity }
                },
                Category = c1.Entity
            });
            await DbContext.Articles.AddAsync(new Article
            {
                Title = "article 3",
                Status = BaseStatus.Enabled,
                ArticleTags = new List<ArticleTag>
                {
                    new ArticleTag { Tag = tag2.Entity }
                },
                Category = c1.Entity
            });
            await DbContext.SaveChangesAsync();

            var svc = new TagService(DbContext, Mapper);
            var result = await svc.GetArticlesDtoAsync("Tag2", 1, 2);

            Assert.AreEqual(1, result.Page);
            Assert.AreEqual(2, result.Size);
            Assert.AreEqual(2, result.TotalItems);
            Assert.AreEqual(1, result.TotalPages);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("article 3", result.Data[0].Title);
            Assert.AreEqual("article 2", result.Data[1].Title);
        }
    }
}
