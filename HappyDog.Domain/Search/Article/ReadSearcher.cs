using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Enums;
using HappyDog.Infrastructure;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HappyDog.Domain.Search.Article
{
    class ReadSearcher : Searcher
    {
        public ReadSearcher(HappyDogContext db, IMapper mapper) : base(db, mapper) { }

        protected override ArticleCategory Category => ArticleCategory.Read;

        public override Regex Regex => new Regex(@"^read:(.+)$");

        public async override Task<Pagination<ArticleDto>> MatchAsync(GroupCollection groups, int page, int size)
        {
            Keyword = groups[1].Value.Trim();
            return await base.MatchAsync(groups, page, size);
        }
    }
}
