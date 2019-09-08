using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Search;
using HappyDog.Domain.Search.Article;
using HappyDog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class ArticleService
    {
        public ArticleService(HappyDogContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        readonly HappyDogContext _db;
        readonly IMapper _mapper;

        public async Task<ArticleDetailDto> GetArticleDetailDtoAsync(int id)
        {
            var article = await _db.Articles.Include(a => a.Category)
                .SingleOrDefaultAsync(a => a.Id == id && a.Status == BaseStatus.Enable);
            article.ViewCount++;
            await _db.SaveChangesAsync();
            return _mapper.Map<ArticleDetailDto>(article);
        }

        public async Task<Pagination<ArticleDto>> GetArticleDtosAsync(int page, int size, ArticleCategory? cid)
        {
            var pagination = new Pagination<ArticleDto>
            {
                Page = page,
                Size = size,
                TotalItems = await _db.Articles
                    .Where(a => a.Status == BaseStatus.Enable)
                    .Where(a => !cid.HasValue || a.CategoryId == (int)cid.Value)
                    .CountAsync(),
            };
            pagination.Data = await _db.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a => a.Status == BaseStatus.Enable)
                .Where(a => !cid.HasValue || a.CategoryId == (int)cid.Value)
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * pagination.Size)
                .Take(pagination.Size)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return pagination;
        }

        public async Task<Pagination<ArticleDto>> SearchAsync(string keyword, int page, int size)
        {
            var searcher = new HappySearcher();
            searcher.Register(new NetSearcher(_db, _mapper));
            searcher.Register(new DatabaseSearcher(_db, _mapper));
            searcher.Register(new WindowsSearcher(_db, _mapper));
            searcher.Register(new ReadSearcher(_db, _mapper));
            searcher.Register(new EssaysSearcher(_db, _mapper));
            searcher.Register(new ArticleSearcher(_db, _mapper));
            return await searcher.SearchAsync(keyword, page, size);
        }

        public async Task<bool> UpdateAsync(int id, Article article)
        {
            var dbArticle = await _db.Articles.FindAsync(id);
            if (dbArticle != null)
            {
                dbArticle.CategoryId = article.CategoryId;
                dbArticle.Title = article.Title;
                dbArticle.Content = article.Content;
                dbArticle.Status = article.Status;
                int total = await _db.SaveChangesAsync();
                return total > 0;
            }
            return false;
        }

        public async Task<Article> AddArticleAsync(Article article)
        {
            article.CreateTime = DateTime.Now;
            await _db.Articles.AddAsync(article);
            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<List<ArticleDto>> GetTopArticleDtosAsync(int count)
        {
            return await _db.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a => a.Status == BaseStatus.Enable)
                .OrderByDescending(a => a.ViewCount)
                .Take(count)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
