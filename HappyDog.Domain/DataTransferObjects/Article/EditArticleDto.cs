using HappyDog.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.Article
{
    public class EditArticleDto
    {
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [MaxLength(200, ErrorMessage = "标题长度不能超过200个字符")]
        public string Title { get; set; }

        [Required(ErrorMessage = "请输入内容")]
        [Display(Name = "内容")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public BaseStatus State { get; set; }
    }
}
