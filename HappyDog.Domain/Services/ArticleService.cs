using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HappyDog.Domain.Services
{
    public class ArticleService
    {
        readonly HappyDogContext db;

        public ArticleService(HappyDogContext db)
        {
            this.db = db;
        }

        public async Task<Article> GetAsync(int id, bool isAuthenticated)
        {
            var article = await db.Articles
                .SingleOrDefaultAsync(a => a.Id == id && (isAuthenticated || a.State == BaseState.Enable));
            if (article != null)
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
    }
}
