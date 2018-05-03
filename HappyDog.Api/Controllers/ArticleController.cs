using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyDog.DataTransferObjects.Article;
using HappyDog.Domain;
using HappyDog.Domain.Enums;
using HappyDog.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HappyDog.Domain.Entities;
using HappyDog.DataTransferObjects.Category;
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

        public async Task<IEnumerable<ArticleDTO>> Get(int? cid, int page = 1)
        {
            var pager = new Pager(page, PageSize);
            return await context.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a =>
                    (User.Identity.IsAuthenticated || a.State == BaseState.Enable)
                    && (cid.HasValue || a.CategoryId == cid.Value)
                )
                .ProjectTo<ArticleDTO>()
                .ToListAsync();

            //if (!string.IsNullOrEmpty(category))
            //{
            //    var cate = await context.Categories.AsNoTracking()
            //          .SingleOrDefaultAsync(c =>
            //              c.Value.Equals(category, StringComparison.CurrentCultureIgnoreCase)
            //              && c.State == BaseState.Enable);
            //    if (cate != null)
            //    {
            //        query.Where(a => a.CategoryId == cate.Id);
            //    }
            //}
            //pager.TotalItems = await query.CountAsync();
            //await query.OrderByDescending(a => a.Id)
            //    .Skip(pager.Skip)
            //    .Take(pager.Size)
            //    .Select(a => new ArticleDto
            //    {
            //        Id = a.Id,
            //        Title = a.Title,
            //        Content = a.Content,
            //        CreateTime = a.CreateTime,
            //        ViewCount = a.ViewCount,
            //        Category = mapper.
            //    })
            //    .ToListAsync()
        }
    }
}