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

namespace HappyDog.Api.Controllers
{
    [Produces("application/json")]
    [Route("article")]
    [Authorize]
    public class ArticleController : Controller
    {
        readonly ArticleService svc;
        readonly IMapper mapper;

        public ArticleController(ArticleService svc, IMapper mapper)
        {
            this.svc = svc;
            this.mapper = mapper;
            PageSize = 20;
        }

        public int PageSize { get; set; }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var article = await svc.GetAsync(id, User.Identity.IsAuthenticated);
            if (article == null)
            {
                return NotFound();
            }
            return Json(mapper.Map<Article, ArticleDto>(article));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> List(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, null)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<HttpBaseResult> Put(int id, [FromBody]EditArticleDto dto)
        {
            await svc.UpdateAsync(id, dto);
            return new HttpBaseResult
            {
                Status = HttpStatusCode.OK,
                Notify = NotifyResult.Success,
                Message = "修改成功"
            };
        }

        [HttpPost]
        [ValidateModel]
        public async Task<HttpDataResult<int>> Post([FromBody]PostArticleDto dto)
        {
            var article = await svc.InsertAsync(dto);
            return new HttpDataResult<int>
            {
                Status = HttpStatusCode.OK,
                Data = article.Id,
                Notify = NotifyResult.Success,
                Message = "添加成功"
            };
        }

        [HttpGet("net")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Net(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, ArticleCategory.Net)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        [HttpGet("db")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Database(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, ArticleCategory.Database)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        [HttpGet("windows")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Windows(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, ArticleCategory.Windows)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        [HttpGet("read")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Read(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, ArticleCategory.Read)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }

        [HttpGet("essays")]
        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Essays(int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, ArticleCategory.Essays)
                .ProjectTo<ArticleSummaryDto>(mapper.ConfigurationProvider);
            return await pager.GetPaginationAsync(query);
        }
    }
}