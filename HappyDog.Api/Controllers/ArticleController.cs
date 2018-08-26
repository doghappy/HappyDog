using AutoMapper;
using System.Threading.Tasks;
using HappyDog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Authorization;
using HappyDog.Domain.Enums;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Api.Filters;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace HappyDog.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("article")]
    [Authorize]
    public class ArticleController : Controller
    {
        readonly ArticleService articleService;
        readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleService"></param>
        /// <param name="mapper"></param>
        public ArticleController(ArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
            PageSize = 20;
        }

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
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var article = await articleService.GetAsync(id, User.Identity.IsAuthenticated);
            if (article == null)
            {
                return NotFound();
            }
            return Json(mapper.Map<Article, ArticleDto>(article));
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> List(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = articleService.Get(User.Identity.IsAuthenticated, null)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="id">文章id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<HttpBaseResult> Put(int id, [FromBody]EditArticleDto dto)
        {
            bool result = await articleService.UpdateAsync(id, dto);
            if (result)
            {
                return new HttpBaseResult
                {
                    NoticeMode = NoticeMode.Success,
                    Message = "修改成功"
                };
            }
            else
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                return new HttpBaseResult
                {
                    NoticeMode = NoticeMode.Warning,
                    Message = "修改失败"
                };
            }
        }

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<HttpBaseResult> Post([FromBody]PostArticleDto dto)
        {
            var article = await articleService.AddAsync(dto);
            if (article == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new HttpBaseResult
                {
                    NoticeMode = NoticeMode.Warning,
                    Message = "添加失败"
                };
            }
            else
            {
                return new HttpDataResult<int>
                {
                    Data = article.Id,
                    NoticeMode = NoticeMode.Success,
                    Message = "添加成功"
                };
            }
        }

        /// <summary>
        /// .Net
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("net")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Net(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = articleService.Get(User.Identity.IsAuthenticated, ArticleCategory.Net)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        /// <summary>
        /// 数据库
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("db")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Database(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = articleService.Get(User.Identity.IsAuthenticated, ArticleCategory.Database)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        /// <summary>
        /// Windows
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("windows")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Windows(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = articleService.Get(User.Identity.IsAuthenticated, ArticleCategory.Windows)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        /// <summary>
        /// 阅读
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("read")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Read(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = articleService.Get(User.Identity.IsAuthenticated, ArticleCategory.Read)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        /// <summary>
        /// 随笔
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("essays")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Essays(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = articleService.Get(User.Identity.IsAuthenticated, ArticleCategory.Essays)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        /// <summary>
        /// 文章搜索
        /// </summary>
        /// <param name="q">搜索关键词，也可有分类（db:sql, windows:win10）</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet("search")]
        [AllowAnonymous]
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
                var pager = new Pager(page, PageSize);
                var query = articleService.Search(User.IsInRole("Owner"), q, pager)
                    .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
                var data = await pager.GetPaginationAsync(query);
                return new HttpDataResult<Pagination<ArticleSummaryDto>>
                {
                    Data = data
                };
            }
        }
    }
}