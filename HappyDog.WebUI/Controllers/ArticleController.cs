using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        readonly ArticleService articleService;

        public ArticleController()
        {
            PageSize = 20;
        }

        public int PageSize { get; set; }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = articleService.Get(User.Identity.IsAuthenticated, null);
            var pager = new Pager(page, PageSize);
            var data = await pager.GetPaginationAsync(query);
            return View(data);
        }
    }
}