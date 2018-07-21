﻿using HappyDog.Domain.Enums;
using System.Linq;
using System.Text.RegularExpressions;

namespace HappyDog.Domain.Search.Article
{
    class NetSearcher : Searcher
    {
        public NetSearcher(HappyDogContext db, bool isOwner) : base(db, isOwner) { }

        protected override ArticleCategory Category => ArticleCategory.Net;

        public override Regex Regex => new Regex(@"^net:(.+)$");

        protected override string Keyword { get; set; }

        public override IOrderedQueryable<Entities.Article> Match(GroupCollection groups)
        {
            Keyword = groups[1].Value.Trim();
            return base.Match(groups);
        }
    }
}