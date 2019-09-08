using HappyDog.Domain.DataTransferObjects.Article;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HappyDog.WebUI.ViewComponents
{
    public class ArticleDtosViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<ArticleDto> articles)
        {
            return View(articles);
        }
    }
}
