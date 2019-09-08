using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Enums;
using HappyDog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HappyDog.Domain.Search.Article
{
    abstract class Searcher : IHappySearchable
    {
        protected Searcher(HappyDogContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        readonly HappyDogContext _db;
        readonly IMapper _mapper;

        public abstract Regex Regex { get; }

        protected abstract ArticleCategory Category { get; }

        public string Keyword { get; protected set; }

        public async virtual Task<Pagination<ArticleDto>> MatchAsync(GroupCollection groups, int page, int size)
        {
            var pagination = new Pagination<ArticleDto>
            {
                Page = page,
                Size = size,
                TotalItems = await _db.Articles.Include(a => a.Category).AsNoTracking()
                    .Where(a =>
                        a.Status == BaseStatus.Enable
                        && a.CategoryId == (int)Category
                        && a.Title.Contains(Keyword, StringComparison.OrdinalIgnoreCase)
                    )
                    .CountAsync()
            };
            pagination.Data = await _db.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a =>
                    a.Status == BaseStatus.Enable
                    && a.CategoryId == (int)Category
                    && a.Title.Contains(Keyword, StringComparison.OrdinalIgnoreCase)
                )
                .OrderByDescending(a => a.Id)
                .Skip((pagination.Page - 1) * pagination.Size)
                .Take(pagination.Size)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return pagination;
        }
    }
}
