using HappyDog.Domain.DataTransferObjects.Category;
using HappyDog.Domain.Enums;
using System;
using System.Collections.Generic;

namespace HappyDog.Domain.DataTransferObjects.Tag
{

    public class TagArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public int ViewCount { get; set; }
        public ArticleCategory CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        //public BaseStatus Status { get; set; }
        //public List<TagDto> Tags { get; set; }
    }
}
