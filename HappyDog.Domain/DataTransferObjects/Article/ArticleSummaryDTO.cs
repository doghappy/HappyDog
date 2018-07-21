using HappyDog.Domain.DataTransferObjects.Category;
using HappyDog.Domain.Enums;
using System;

namespace HappyDog.Domain.DataTransferObjects.Article
{
    public class ArticleSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public int ViewCount { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public BaseStatus State { get; set; }
    }
}
