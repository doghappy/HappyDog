using HappyDog.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.Article
{
    public class PostArticleDto
    {
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [MaxLength(200, ErrorMessage = "标题长度不能超过200个字符")]
        public string Title { get; set; }

        [Display(Name = "内容")]
        public string Content { get; set; }

        [Required(ErrorMessage = "请选择分类")]
        [Display(Name = "分类")]
        public ArticleCategory CategoryId { get; set; }

        public BaseStatus Status { get; set; }

        public List<string> TagNames { get; set; }
    }
}
