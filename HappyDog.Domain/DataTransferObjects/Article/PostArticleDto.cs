using HappyDog.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.Article
{
    public class PostArticleDto : IValidatableObject
    {
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [MaxLength(200, ErrorMessage = "标题长度不能超过200个字符")]
        public string Title { get; set; }

        [Required(ErrorMessage = "请输入内容")]
        [Display(Name = "内容")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public BaseStatus Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CategoryId < 1 || CategoryId > 5)
            {
                yield return new ValidationResult("无效的分类", new[] { nameof(CategoryId) });
            }
        }
    }
}
