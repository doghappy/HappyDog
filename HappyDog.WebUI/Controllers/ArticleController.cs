using System.Threading.Tasks;
using HappyDog.Domain.Enums;
using HappyDog.Domain.IServices;
using HappyDog.Domain.Services;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleController(IArticleService articleService, CategoryService categoryService)
        {
            PageSize = 20;
            _articleService = articleService;
            _categoryService = categoryService;
        }

        readonly IArticleService _articleService;
        readonly CategoryService _categoryService;

        public int PageSize { get; set; }

        public async Task<IActionResult> Index(int page = 1)
        {
            var data = await _articleService.GetArticlesDtoAsync(page, PageSize, null);
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var article = await _articleService.GetEnabledArticleDetailDtoAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                if (article.Content != null)
                {
                    var pipeline = new MarkdownPipelineBuilder()
                        .UsePipeTables()
                        .Build();
                    article.Content = Markdown.ToHtml(article.Content, pipeline);
                }
                return View(article);
            }
        }

        public async Task<IActionResult> Net(int page = 1)
        {
            ViewBag.CategoryActive = true;
            var data = await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Net);
            return View(data);
        }

        public async Task<IActionResult> Java(int page = 1)
        {
            ViewBag.CategoryActive = true;
            var data = await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Java);
            return View(data);
        }

        public async Task<IActionResult> Database(int page = 1)
        {
            ViewBag.CategoryActive = true;
            var data = await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Database);
            return View(data);
        }

        public async Task<IActionResult> Windows(int page = 1)
        {
            ViewBag.CategoryActive = true;
            var data = await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Windows);
            return View(data);
        }

        public async Task<IActionResult> Read(int page = 1)
        {
            ViewBag.CategoryActive = true;
            var data = await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Read);
            return View(data);
        }

        public async Task<IActionResult> Essays(int page = 1)
        {
            ViewBag.CategoryActive = true;
            var data = await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Essays);
            return View(data);
        }

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            ViewBag.SearchValue = q;
            if (string.IsNullOrWhiteSpace(q))
            {
                ViewBag.Message = "请输入关键词";
                var hotData = await _articleService.GetTopArticlesDtoAsync(20);
                return View("EmptySearch", hotData);
            }
            else
            {
                var pagination = await _articleService.SearchAsync(q, page, PageSize);
                if (pagination.TotalItems == 0)
                {
                    ViewBag.Message = $"未找到与 \"{q}\" 相关的数据";
                    var hotData = await _articleService.GetTopArticlesDtoAsync(20);
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