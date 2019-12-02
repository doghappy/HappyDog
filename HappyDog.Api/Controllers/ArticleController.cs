using System.Threading.Tasks;
using HappyDog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Enums;
using System.Net;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.IServices;

namespace HappyDog.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleService"></param>
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
            PageSize = 20;
        }

        readonly IArticleService _articleService;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var article = await _articleService.GetArticleDetailDtoAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Json(article);
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Pagination<ArticleDto>> List(int page = 1)
        {
            return await _articleService.GetArticlesDtoAsync(page, PageSize, null);
        }

        /// <summary>
        /// .Net
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("net")]
        public async Task<Pagination<ArticleDto>> Net(int page = 1)
        {
            return await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Net);
        }

        /// <summary>
        /// 数据库
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("db")]
        public async Task<Pagination<ArticleDto>> Database(int page = 1)
        {
            return await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Database);
        }

        /// <summary>
        /// Windows
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("windows")]
        public async Task<Pagination<ArticleDto>> Windows(int page = 1)
        {
            return await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Windows);
        }

        /// <summary>
        /// 阅读
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("read")]
        public async Task<Pagination<ArticleDto>> Read(int page = 1)
        {
            return await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Read);
        }

        /// <summary>
        /// 随笔
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("essays")]
        public async Task<Pagination<ArticleDto>> Essays(int page = 1)
        {
            return await _articleService.GetArticlesDtoAsync(page, PageSize, ArticleCategory.Essays);
        }

        /// <summary>
        /// 文章搜索
        /// </summary>
        /// <param name="q">搜索关键词，也可有分类（db:sql, windows:win10）</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<HttpBaseResult> Search(string q, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new HttpBaseResult
                {
                    NoticeMode = NoticeMode.Info,
                    Message = "请输入关键词"
                };
            }
            else
            {
                var data = await _articleService.SearchAsync(q, page, PageSize);
                return new HttpDataResult<Pagination<ArticleDto>>
                {
                    Data = data
                };
            }
        }
    }
}