using HappyDog.Domain.DataTransferObjects.Article;
using HappyDog.Infrastructure;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HappyDog.Domain.Search
{
    public interface IHappySearchable
    {
        Regex Regex { get; }
        string Keyword { get; }
        Task<Pagination<ArticleDto>> MatchAsync(GroupCollection groups, int page, int size);
    }
}
