﻿using HappyDog.Domain.Enums;
using System.Linq;
using System.Text.RegularExpressions;

namespace HappyDog.Domain.Search.Article
{
    class ReadSearcher : Searcher
    {
        public ReadSearcher(HappyDogContext db, bool isOwner) : base(db, isOwner) { }

        protected override ArticleCategory Category => ArticleCategory.Read;

        public override Regex Regex => new Regex(@"^read:(.+)$");

        public override IOrderedQueryable<Entities.Article> Match(GroupCollection groups)
        {
            Keyword = groups[1].Value.Trim();
            return base.Match(groups);
        }
    }
}
