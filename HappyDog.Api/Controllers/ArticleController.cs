using AutoMapper;
using System.Threading.Tasks;
using HappyDog.DataTransferObjects.Article;
using HappyDog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Services;

namespace HappyDog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/article")]
    public class ArticleController : Controller
    {
        readonly ArticleService svc;
        readonly IMapper mapper;

        public ArticleController(ArticleService svc, IMapper mapper)
        {
            this.svc = svc;
            this.mapper = mapper;
        }

        public int PageSize => 20;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await svc.GetAsync(id, User.Identity.IsAuthenticated);
            if (article == null)
            {
                return NotFound();
            }
            return Json(mapper.Map<Article, ArticleDTO>(article));
        }

        public async Task<Pagination<ArticleSummaryDTO>> Get(int? cid, int page = 1)
        {
            bool flag = svc == null;
            var pager = new Pager(page, PageSize);
            var query = svc.Get(User.Identity.IsAuthenticated, cid)
                .ProjectTo<ArticleSummaryDTO>();
            return await pager.GetPaginationAsync(query);
        }
    }
}