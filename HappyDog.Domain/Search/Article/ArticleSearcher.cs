using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Enums;
using HappyDog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HappyDog.Domain.Search.Article
{
    class ArticleSearcher : IHappySearchable
    {
        public ArticleSearcher(HappyDogContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        readonly HappyDogContext _db;
        readonly IMapper _mapper;

        public Regex Regex => new Regex("^.*$");

        public string Keyword { get; protected set; }

        public async Task<Pagination<ArticleDto>> MatchAsync(GroupCollection groups, int page, int size)
        {
            Keyword = groups[0].Value.Trim();
            if (Keyword.Length > 0)
            {
                var pagination = new Pagination<ArticleDto>
                {
                    Page = page,
                    Size = size,
                    TotalItems = await _db.Articles.AsNoTracking()
                        .Where(a => a.Status == BaseStatus.Enabled && EF.Functions.Like(a.Title, $"%{Keyword}%"))
                        .CountAsync()
                };
                pagination.Data = await _db.Articles.Include(a => a.Category).AsNoTracking()
                    .Where(a => a.Status == BaseStatus.Enabled && EF.Functions.Like(a.Title, $"%{Keyword}%"))
                    .OrderByDescending(a => a.Id)
                    .Skip((pagination.Page - 1) * pagination.Size)
                    .Take(pagination.Size)
                    .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return pagination;
            }
            else
            {
                return null;
            }
        }
    }
}
