using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
using HappyDog.Infrastructure;
using HappyDog.WebUI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyDog.WebUI.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
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
        public async Task<IActionResult> Get(int id)
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
        public async Task<Pagination<ArticleSummaryDto>> Get(ArticleCategory? cid, int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, cid)
                .ProjectTo<ArticleSummaryDto>();
            return await pager.GetPaginationAsync(query);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<HttpBaseResult> Put(int id, [FromBody]EditArticleDto dto)
        {
            await svc.UpdateAsync(id, dto);
            return new HttpBaseResult
            {
                Code = CodeResult.OK,
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
                Code = CodeResult.OK,
                Data = article.Id,
                Notify = NotifyResult.Success,
                Message = "添加成功"
            };
        }
    }
}