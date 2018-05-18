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

namespace HappyDog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/article")]
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
        public async Task<IActionResult> Get(int id)
        {
            var article = await svc.GetAsync(id, User.Identity.IsAuthenticated);
            if (article == null)
            {
                return NotFound();
            }
            return Json(mapper.Map<Article, ArticleDto>(article));
        }

        [AllowAnonymous]
        public async Task<Pagination<ArticleSummaryDto>> Get(int? cid, int page = 1)
        {
            bool flag = svc == null;
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, cid)
                .ProjectTo<ArticleSummaryDto>();
            return await pager.GetPaginationAsync(query);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<HttpBaseResult> Put(int id, [FromBody]PutArticleDto dto)
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