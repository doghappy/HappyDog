using System.Threading.Tasks;
using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleController(IMapper mapper, ArticleService articleService, CategoryService categoryService)
        {
            PageSize = 20;
            this.mapper = mapper;
            this.articleService = articleService;
            this.categoryService = categoryService;
        }

        readonly ArticleService articleService;
        readonly CategoryService categoryService;
        readonly IMapper mapper;

        public int PageSize { get; set; }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var data = await articleService.Get(User.IsInRole("Owner"), pager, null);
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var article = await articleService.GetAsync(id, User.IsInRole("Owner"));
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                return View(article);
            }
        }

        public async Task<IActionResult> Net(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var data = await articleService.Get(User.IsInRole("Owner"), pager, ArticleCategory.Net);
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<IActionResult> Database(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var data = await articleService.Get(User.IsInRole("Owner"), pager, ArticleCategory.Database);
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<IActionResult> Windows(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var data = await articleService.Get(User.IsInRole("Owner"), pager, ArticleCategory.Windows);
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<IActionResult> Read(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var data = await articleService.Get(User.IsInRole("Owner"), pager, ArticleCategory.Read);
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<IActionResult> Essays(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var data = await articleService.Get(User.IsInRole("Owner"), pager, ArticleCategory.Essays);
            ViewBag.Pager = pager;
            return View(data);
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await articleService.GetAsync(id, true);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Categories = await categoryService.GetCategoriesAsync();
                var model = mapper.Map<EditArticleDto>(article);
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm]EditArticleDto dto)
        {
            if (ModelState.IsValid)
            {
                await articleService.UpdateAsync(id, dto);
                return RedirectToAction("Detail", new { id });
            }
            else
            {
                ViewBag.Categories = await categoryService.GetCategoriesAsync();
                return View(dto);
            }
        }

        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Post()
        {
            ViewBag.Categories = await categoryService.GetCategoriesAsync();
            var article = new PostArticleDto
            {
                Status = BaseStatus.Disable
            };
            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromForm]PostArticleDto dto)
        {
            if (ModelState.IsValid)
            {
                var article = await articleService.InsertAsync(dto);
                return RedirectToAction("Detail", new { id = article.Id });
            }
            else
            {
                ViewBag.Categories = await categoryService.GetCategoriesAsync();
                return View(dto);
            }
        }

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            ViewBag.SearchValue = q;
            if (string.IsNullOrWhiteSpace(q))
            {
                ViewBag.Message = "请输入关键词";
                var hotData = await articleService.GetHotAsync(20);
                return View("EmptySearch", hotData);
            }
            else
            {
                var pager = new Pager(page, PageSize);
                var data = await articleService.Search(User.IsInRole("Owner"), q, pager);
                if (data.Count == 0)
                {
                    ViewBag.Message = $"未找到与 \"{q}\" 相关的数据";
                    var hotData = await articleService.GetHotAsync(20);
                    return View("EmptySearch", hotData);
                }
                else
                {
                    ViewBag.Pager = pager;
                    return View(data);
                }
            }
        }
    }
}