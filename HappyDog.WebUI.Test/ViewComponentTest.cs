using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.WebUI.ViewComponents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HappyDog.WebUI.Test
{
    [TestClass]
    public class ViewComponentTest : TestBase
    {
        [TestMethod]
        public void ArticleListVcTest()
        {
            var vc = new ArticleListViewComponent
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext
                    {
                        HttpContext = new DefaultHttpContext()
                    }
                }
            };



            var articles = new List<ArticleSummaryDto>
            {
                new ArticleSummaryDto { Id = 1 },
                new ArticleSummaryDto { Id = 2 },
                new ArticleSummaryDto { Id = 3 }
            };
            var result = vc.Invoke(articles) as ViewViewComponentResult;
            var model = result.ViewData.Model as List<ArticleSummaryDto>;

            Assert.AreEqual(3, model.Count);
        }
    }
}
