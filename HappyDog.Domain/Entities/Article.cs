using HappyDog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDog.Domain.Entities
{
    [Table("Articles")]
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public ArticleCategory CategoryId { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public int ViewCount { get; set; }

        public BaseStatus Status { get; set; }

        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }
    }
}
