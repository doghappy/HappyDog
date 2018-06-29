using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using HeyRed.MarkdownSharp;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        readonly ArticleService articleService;

        public ArticleController(ArticleService articleService)
        {
            PageSize = 20;
            this.articleService = articleService;
        }

        public int PageSize { get; set; }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var data = await articleService.Get(User.Identity.IsAuthenticated, pager, null);
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var article = await articleService.GetAsync(id, User.Identity.IsAuthenticated);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                var markdown = new Markdown();
                article.Content = markdown.Transform(article.Content);
                return View(article);
            }
        }

        public async Task<IActionResult> Net()
        {
            return View();
        }
    }
}