using AutoMapper;
using AutoMapper.QueryableExtensions;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.IServices;
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
    public class ArticleService : IArticleService
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
            var article = await _db.Articles
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.ArticleTags)
                .ThenInclude(a => a.Tag)
                .SingleOrDefaultAsync(a => a.Id == id);
            if (article == null)
            {
                return default;
            }
            else
            {
                return _mapper.Map<ArticleDetailDto>(article);
            }
        }

        public async Task<ArticleDetailDto> GetEnabledArticleDetailDtoAsync(int id)
        {
            var article = await _db.Articles
                .Include(a => a.Category)
                .Include(a => a.ArticleTags)
                .ThenInclude(a => a.Tag)
                .SingleOrDefaultAsync(a => a.Id == id && a.Status == BaseStatus.Enabled);
            if (article == null)
            {
                return default;
            }
            else
            {
                article.ViewCount++;
                await _db.SaveChangesAsync();
                return _mapper.Map<ArticleDetailDto>(article);
            }
        }

        public async Task<Pagination<ArticleDto>> GetArticlesDtoAsync(int page, int size, ArticleCategory? cid)
        {
            var query = _db.Articles
                .AsNoTracking()
                .Where(a => a.Status == BaseStatus.Enabled)
                .Where(a => !cid.HasValue || a.CategoryId == cid.Value);

            var pagination = new Pagination<ArticleDto>
            {
                Page = page,
                Size = size,
                TotalItems = await query.CountAsync(),
            };
            pagination.Data = await query
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

        public async Task<List<ArticleDto>> GetTopArticlesDtoAsync(int count)
        {
            return await _db.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a => a.Status == BaseStatus.Enabled)
                .OrderByDescending(a => a.ViewCount)
                .Take(count)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ArticleDto>> GetDisabledArticlesDtoAsync()
        {
            return await _db.Articles.AsNoTracking()
                .Include(a => a.Category)
                .Where(a => a.Status == BaseStatus.Disabled)
                .OrderByDescending(a => a.Id)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        private async Task AssignTagsAsync(Article article, IEnumerable<string> tagNames)
        {
            var list = new List<string>();
            foreach (var item in tagNames)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    string name = item.Trim();
                    if (!list.Contains(name, StringComparer.CurrentCultureIgnoreCase))
                    {
                        list.Add(name);
                    }
                }
            }

            foreach (var item in list)
            {
                string name = item.Trim();
                var tag = await _db.Tags.SingleOrDefaultAsync(t => t.Name.ToLower() == item.Trim().ToLower());
                if (tag == null)
                {
                    article.ArticleTags.Add(new ArticleTag { Tag = new Tag { Name = name } });
                }
                else
                {
                    article.ArticleTags.Add(new ArticleTag { TagId = tag.Id });
                }
            }
        }

        public async Task<ArticleDetailDto> PostAsync(PostArticleDto dto)
        {
            var article = _mapper.Map<Article>(dto);
            article.CreateTime = DateTimeOffset.Now;
            article.ArticleTags = new List<ArticleTag>();
            await _db.Articles.AddAsync(article);
            if (dto.TagNames.Count > 0)
            {
                await AssignTagsAsync(article, dto.TagNames);
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<ArticleDetailDto>(article);
        }

        public async Task<ArticleDetailDto> PutAsync(int id, PutArticleDto dto)
        {
            var article = await _db.Articles.Include(a => a.ArticleTags).SingleOrDefaultAsync(a => a.Id == id);
            if (article == null)
            {
                return default;
            }
            else
            {
                article.CategoryId = dto.CategoryId;
                article.Title = dto.Title;
                article.Content = dto.Content;
                article.Status = dto.Status;
                article.ArticleTags.Clear();
                await AssignTagsAsync(article, dto.TagNames);
                await _db.SaveChangesAsync();
                return _mapper.Map<ArticleDetailDto>(article);
            }
        }
    }
}
