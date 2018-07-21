using System;
using HappyDog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;

namespace HappyDog.Domain.Search.Article
{
    abstract class Searcher : IHappySearchable<IOrderedQueryable<Entities.Article>>
    {
        protected Searcher(HappyDogContext db, bool isOwner)
        {
            this.db = db;
            this.isOwner = isOwner;
        }

        readonly HappyDogContext db;
        readonly bool isOwner;

        public abstract Regex Regex { get; }

        protected abstract ArticleCategory Category { get; }

        protected abstract string Keyword { get; set; }

        public virtual IOrderedQueryable<Entities.Article> Match(GroupCollection groups)
        {
            return db.Articles.Include(a => a.Category).AsNoTracking()
                   .Where(a =>
                       (isOwner || a.Status == BaseStatus.Enable)
                       && a.CategoryId == (int)Category
                       && EF.Functions.Like(a.Title, $"%{Keyword}%")
                   )
                   .OrderByDescending(a => a.Id);
        }
    }
}
