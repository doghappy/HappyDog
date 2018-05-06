using System.Collections.Generic;

namespace HappyDog.Infrastructure
{
    public class Pagination<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalItems { get; set; }
    }
}
