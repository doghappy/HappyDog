using AutoMapper;
using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Domain.Enums;
using HappyDog.Infrastructure;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HappyDog.Domain.Search.Article
{
    class NetSearcher : Searcher
    {
        public NetSearcher(HappyDogContext db, IMapper mapper) : base(db, mapper) { }

        protected override ArticleCategory Category => ArticleCategory.Net;

        public override Regex Regex => new Regex(@"^net:(.+)$");

        public async override Task<Pagination<ArticleDto>> MatchAsync(GroupCollection groups, int page, int size)
        {
            Keyword = groups[1].Value.Trim();
            return await base.MatchAsync(groups, page, size);
        }
    }
}
