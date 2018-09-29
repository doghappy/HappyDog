using HappyDog.Domain.DataTransferObjects.Article;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HappyDog.WebUI.ViewComponents
{
    public class ArticleListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<ArticleSummaryDto> articles)
        {
            return View(articles);
        }
    }
}
