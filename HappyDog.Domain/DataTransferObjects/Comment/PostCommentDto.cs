using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.Comment
{
    public class PostCommentDto
    {
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "请输入昵称")]
        [Display(Name = "昵称")]
        [MaxLength(50, ErrorMessage = "标题长度不能超过200个字符")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入邮箱")]
        [Display(Name = "邮箱")]
        [MaxLength(50, ErrorMessage = "邮箱长度不能超过200个字符")]
        public string Email { get; set; }

        [Required(ErrorMessage = "请输入评论")]
        [Display(Name = "评论")]
        [MaxLength(1000, ErrorMessage = "评论长度不能超过1000个字符")]
        public string Content { get; set; }
    }
}
