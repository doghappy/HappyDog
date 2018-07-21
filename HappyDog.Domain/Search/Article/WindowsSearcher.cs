using HappyDog.Domain.Enums;
using System.Linq;
using System.Text.RegularExpressions;

namespace HappyDog.Domain.Search.Article
{
    class WindowsSearcher : Searcher
    {
        public WindowsSearcher(HappyDogContext db, bool isOwner) : base(db, isOwner) { }

        protected override ArticleCategory Category => ArticleCategory.Windows;

        public override Regex Regex => new Regex(@"^windows:(.+)$");

        protected override string Keyword { get; set; }

        public override IOrderedQueryable<Entities.Article> Match(GroupCollection groups)
        {
            Keyword = groups[1].Value.Trim();
            return base.Match(groups);
        }
    }
}
