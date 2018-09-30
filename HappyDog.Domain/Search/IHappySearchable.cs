using System.Text.RegularExpressions;

namespace HappyDog.Domain.Search
{
    public interface IHappySearchable<T>
    {
        Regex Regex { get; }
        string Keyword { get; }
        T Match(GroupCollection groups);
    }
}
