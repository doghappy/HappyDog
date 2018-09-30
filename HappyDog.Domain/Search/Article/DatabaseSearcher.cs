using HappyDog.Domain.Enums;
using System.Linq;
using System.Text.RegularExpressions;

namespace HappyDog.Domain.Search.Article
{
    class DatabaseSearcher : Searcher
    {
        public DatabaseSearcher(HappyDogContext db, bool isOwner) : base(db, isOwner) { }

        protected override ArticleCategory Category => ArticleCategory.Database;

        public override Regex Regex => new Regex(@"^db:(.+)$");

        public override IOrderedQueryable<Entities.Article> Match(GroupCollection groups)
        {
            Keyword = groups[1].Value.Trim();
            return base.Match(groups);
        }
    }
}
