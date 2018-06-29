using HappyDog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HappyDog.WebUI.ViewComponents
{
    public class ArticleListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<Article> articles)
        {
            return View(articles);
        }
    }
}
