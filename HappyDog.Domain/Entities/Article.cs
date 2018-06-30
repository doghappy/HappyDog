using HappyDog.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDog.Domain.Entities
{
    [Table("Articles")]
    public class Article
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入标题")]
        [MaxLength(200, ErrorMessage = "标题长度不能超过200个字符")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "内容")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreateTime { get; set; }

        public int ViewCount { get; set; }

        public BaseState State { get; set; }

        [Display(Name = "分类")]
        public Category Category { get; set; }
    }
}
