using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyDog.Domain.Search
{
    public class HappySearcher
    {
        public HappySearcher()
        {
            searchers = new List<IHappySearchable>();
        }

        readonly List<IHappySearchable> searchers;

        public void Register(IHappySearchable searcher)
        {
            searchers.Add(searcher);
        }

        public async Task<Pagination<ArticleDto>> SearchAsync(string q, int page, int size)
        {
            foreach (var item in searchers)
            {
                if (item.Regex.IsMatch(q))
                {
                    var groups = item.Regex.Match(q).Groups;
                    return await item.MatchAsync(groups, page, size);
                }
            }
            return default;
        }
    }
}
