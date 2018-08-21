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
        public ArticleService(HappyDogContext db)
        {
            this.db = db;
        }

        readonly HappyDogContext db;

        public async Task<Article> GetAsync(int id, bool isOwner)
        {
            var article = await db.Articles.Include(a => a.Category)
                .SingleOrDefaultAsync(a => a.Id == id && (isOwner || a.Status == BaseStatus.Enable));
            if (article?.Status == BaseStatus.Enable)
            {
                article.ViewCount++;
                await db.SaveChangesAsync();
            }
            return article;
        }

        public IQueryable<Article> Get(bool isOwner, ArticleCategory? cid)
        {
            return db.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a =>
                    (isOwner || a.Status == BaseStatus.Enable)
                    && (!cid.HasValue || a.CategoryId == (int?)cid.Value)
                )
                .OrderByDescending(a => a.Id);
        }

        public async Task<List<Article>> Get(bool isOwner, Pager pager, ArticleCategory? cid)
        {
            var query = Get(isOwner, cid);
            pager.TotalItems = await query.CountAsync();
            return await query.Skip(pager.Skip).Take(pager.Size).ToListAsync();
        }

        public IQueryable<Article> Search(bool isOwner, string keyword, Pager pager)
        {
            var searcher = new HappySearcher<IOrderedQueryable<Article>>();
            searcher.Register(new NetSearcher(db, isOwner));
            searcher.Register(new DatabaseSearcher(db, isOwner));
            searcher.Register(new WindowsSearcher(db, isOwner));
            searcher.Register(new ReadSearcher(db, isOwner));
            searcher.Register(new EssaysSearcher(db, isOwner));
            searcher.Register(new ArticleSearcher(db, isOwner));
            return searcher.Search(keyword);
        }

        public async Task UpdateAsync(int id, EditArticleDto dto)
        {
            var article = await db.Articles.FindAsync(id);
            if (article != null)
            {
                article.CategoryId = dto.CategoryId;
                article.Title = dto.Title;
                article.Content = dto.Content;
                article.Status = dto.Status;
                await db.SaveChangesAsync();
            }
        }

        public async Task<Article> InsertAsync(PostArticleDto dto)
        {
            var article = new Article
            {
                CategoryId = dto.CategoryId,
                Content = dto.Content,
                CreateTime = DateTime.Now,
                Status = dto.Status,
                Title = dto.Title
            };
            await db.Articles.AddAsync(article);
            await db.SaveChangesAsync();
            return article;
        }

        public async Task<List<Article>> GetHotAsync(int count)
        {
            return await db.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a => a.Status == BaseStatus.Enable)
                .OrderByDescending(a => a.ViewCount)
                .Take(count)
                .ToListAsync();
        }
    }
}
