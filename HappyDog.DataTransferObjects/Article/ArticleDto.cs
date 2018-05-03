using HappyDog.DataTransferObjects.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyDog.DataTransferObjects.Article
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int ViewCount { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
