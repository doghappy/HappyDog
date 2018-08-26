using HappyDog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HappyDog.Domain.DataTransferObjects.Article
{
    public class EditArticleDto : IValidatableObject
    {
        public EditArticleDto(HappyDogContext db)
        {
            this.db = db;
        }

        readonly HappyDogContext db;

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
            bool isValidCateId = db.Categories.AsNoTracking()
                   .Any(c => c.Status == BaseStatus.Enable && c.Id == CategoryId);
            if (!isValidCateId)
            {
                yield return new ValidationResult(nameof(CategoryId), new[] { "无效的分类" });
            }
        }
    }
}
