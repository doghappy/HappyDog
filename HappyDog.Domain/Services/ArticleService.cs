using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Article> GetAsync(int id, bool isAuthenticated)
        {
            var article = await db.Articles.Include(a => a.Category)
                .SingleOrDefaultAsync(a => a.Id == id && (isAuthenticated || a.State == BaseState.Enable));
            if (article?.State == BaseState.Enable)
            {
                article.ViewCount++;
                await db.SaveChangesAsync();
            }
            return article;
        }

        public IQueryable<Article> Get(bool isAuthenticated, int? cid)
        {
            return db.Articles.Include(a => a.Category).AsNoTracking()
                .Where(a =>
                    (isAuthenticated || a.State == BaseState.Enable)
                    && (!cid.HasValue || a.CategoryId == cid.Value)
                )
                .OrderByDescending(a => a.Id);
        }

        public async Task UpdateAsync(int id, PutArticleDto dto)
        {
            var article = await db.Articles.FindAsync(id);
            if (article != null)
            {
                article.CategoryId = dto.CategoryId;
                article.Title = dto.Title;
                article.Content = dto.Content;
                article.State = dto.State;
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
                State = dto.State,
                Title = dto.Title
            };
            await db.Articles.AddAsync(article);
            await db.SaveChangesAsync();
            return article;
        }
    }
}
