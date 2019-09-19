using System.Collections.Generic;

namespace HappyDog.WindowsUI.Models
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
