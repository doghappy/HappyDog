using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using HappyDog.DataTransferObjects.Article;
using HappyDog.Domain;
using HappyDog.Domain.Enums;
using HappyDog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace HappyDog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/article")]
    public class ArticleController : Controller
    {
        readonly HappyDogContext context;
        readonly IMapper mapper;

        public ArticleController(HappyDogContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int PageSize => 20;

        public async Task<Pagination<ArticleDTO>> Get(int? cid, int page = 1)
        {
            var pager = new Pager(page, PageSize);
            var query = context.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a =>
                    (User.Identity.IsAuthenticated || a.State == BaseState.Enable)
                    && (!cid.HasValue || a.CategoryId == cid.Value)
                )
                .OrderByDescending(a => a.Id)
                .ProjectTo<ArticleDTO>();
            return await pager.GetPaginationAsync(query);
        }
    }
}