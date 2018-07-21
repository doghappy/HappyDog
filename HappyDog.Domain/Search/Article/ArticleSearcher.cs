using System.Linq;
using System.Text.RegularExpressions;
using HappyDog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HappyDog.Domain.Search.Article
{
    class ArticleSearcher : IHappySearchable<IOrderedQueryable<Entities.Article>>
    {
        public ArticleSearcher(HappyDogContext db, bool isOwner)
        {
            this.db = db;
            this.isOwner = isOwner;
        }

        readonly HappyDogContext db;
        readonly bool isOwner;

        public Regex Regex => new Regex("^.*$");

        public IOrderedQueryable<Entities.Article> Match(GroupCollection groups)
        {
            string keyword = groups[0].Value.Trim();
            if (keyword.Length > 0)
            {
                return db.Articles.Include(a => a.Category).AsNoTracking()
                    .Where(a =>
                        (isOwner || a.State == BaseState.Enable)
                        && a.Title.Contains(keyword)
                    )
                    .OrderByDescending(a => a.Id);
            }
            else
            {
                return null;
            }
        }
    }
}
