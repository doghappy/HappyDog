using HappyDog.Domain.Enums;
using System.Linq;
using System.Text.RegularExpressions;

namespace HappyDog.Domain.Search.Article
{
    class EssaysSearcher : Searcher
    {
        public EssaysSearcher(HappyDogContext db, bool isOwner) : base(db, isOwner) { }

        protected override ArticleCategory Category => ArticleCategory.Essays;

        public override Regex Regex => new Regex(@"^essays:(.+)$");

        public override IOrderedQueryable<Entities.Article> Match(GroupCollection groups)
        {
            Keyword = groups[1].Value.Trim();
            return base.Match(groups);
        }
    }
}
