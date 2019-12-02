using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.WebUI.ViewComponents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class ArticleDtosViewComponentTest : TestBase
    {
        [TestMethod]
        public void ArticleListVcTest()
        {
            var vc = new ArticleDtosViewComponent
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext
                    {
                        HttpContext = new DefaultHttpContext()
                    }
                }
            };



            var articles = new List<ArticleDto>
            {
                new ArticleDto { Id = 1 },
                new ArticleDto { Id = 2 },
                new ArticleDto { Id = 3 }
            };
            var result = vc.Invoke(articles) as ViewViewComponentResult;
            var model = result.ViewData.Model as List<ArticleDto>;

            Assert.AreEqual(3, model.Count);
        }
    }
}
