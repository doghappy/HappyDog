using System.Threading.Tasks;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleController(ArticleService articleService, CategoryService categoryService)
        {
            PageSize = 20;
            this.articleService = articleService;
            this.categoryService = categoryService;
        }

        readonly ArticleService articleService;
        readonly CategoryService categoryService;

        public int PageSize { get; set; }

        public async Task<IActionResult> Index(int page = 1)
        {
            var data = await articleService.GetArticleDtosAsync(page, PageSize, null);
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var article = await articleService.GetArticleDetailDtoAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                article.Content = Markdown.ToHtml(article.Content);
                return View(article);
            }
        }

        public async Task<IActionResult> Net(int page = 1)
        {
            var data = await articleService.GetArticleDtosAsync(page, PageSize, ArticleCategory.Net);
            return View(data);
        }

        public async Task<IActionResult> Database(int page = 1)
        {
            var data = await articleService.GetArticleDtosAsync(page, PageSize, ArticleCategory.Database);
            return View(data);
        }

        public async Task<IActionResult> Windows(int page = 1)
        {
            var data = await articleService.GetArticleDtosAsync(page, PageSize, ArticleCategory.Windows);
            return View(data);
        }

        public async Task<IActionResult> Read(int page = 1)
        {
            var data = await articleService.GetArticleDtosAsync(page, PageSize, ArticleCategory.Read);
            return View(data);
        }

        public async Task<IActionResult> Essays(int page = 1)
        {
            var data = await articleService.GetArticleDtosAsync(page, PageSize, ArticleCategory.Essays);
            return View(data);
        }

        //[Authorize(Roles = "Owner")]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var article = await articleService.GetAsync(id, true);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        ViewBag.Categories = await categoryService.GetCategoriesAsync();
        //        return View(article);
        //    }
        //}

        //[HttpPost]
        //[Authorize(Roles = "Owner")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [FromForm]Article dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await articleService.UpdateAsync(id, dto);
        //        return RedirectToAction("Detail", new { id });
        //    }
        //    ViewBag.Categories = await categoryService.GetCategoriesAsync();
        //    return View(dto);
        //}

        //[Authorize(Roles = "Owner")]
        //public async Task<IActionResult> Post()
        //{
        //    ViewBag.Categories = await categoryService.GetCategoriesAsync();
        //    var article = new Article
        //    {
        //        Status = BaseStatus.Disable
        //    };
        //    return View(article);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Owner")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Post([FromForm]Article dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var article = await articleService.AddArticleAsync(dto);
        //        if (article != null)
        //        {
        //            return RedirectToAction("Detail", new { id = article.Id });
        //        }
        //    }
        //    ViewBag.Categories = await categoryService.GetCategoriesAsync();
        //    return View(dto);
        //}

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            ViewBag.SearchValue = q;
            if (string.IsNullOrWhiteSpace(q))
            {
                ViewBag.Message = "请输入关键词";
                var hotData = await articleService.GetTopArticleDtosAsync(20);
                return View("EmptySearch", hotData);
            }
            else
            {
                var pagination = await articleService.SearchAsync(q, page, PageSize);
                if (pagination.TotalItems == 0)
                {
                    ViewBag.Message = $"未找到与 \"{q}\" 相关的数据";
                    var hotData = await articleService.GetTopArticleDtosAsync(20);
                    return View("EmptySearch", hotData);
                }
                else
                {
                    return View(pagination);
                }
            }
        }
    }
}