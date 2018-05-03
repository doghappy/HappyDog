using HappyDog.DataTransferObjects.Category;
using System;

namespace HappyDog.DataTransferObjects.Article
{
    public class ArticleSummaryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public int ViewCount { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
