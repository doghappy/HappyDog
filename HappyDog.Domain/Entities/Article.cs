﻿using HappyDog.Domain.Enums;
using System;
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
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreateTime { get; set; }

        public int ViewCount { get; set; }

        public BaseState State { get; set; }

        [Display(Name = "分类")]
        public Category Category { get; set; }
    }
}
