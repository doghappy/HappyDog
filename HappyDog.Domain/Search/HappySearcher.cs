using System.Collections.Generic;

namespace HappyDog.Domain.Search
{
    public class HappySearcher<T>
    {
        public HappySearcher()
        {
            searchers = new List<IHappySearchable<T>>();
        }

        readonly List<IHappySearchable<T>> searchers;

        public void Register(IHappySearchable<T> searcher)
        {
            searchers.Add(searcher);
        }

        public T Search(string keyword)
        {
            foreach (var item in searchers)
            {
                if (item.Regex.IsMatch(keyword))
                {
                    var groups = item.Regex.Match(keyword).Groups;
                    return item.Match(groups);
                }
            }
            return default(T);
        }
    }
}
