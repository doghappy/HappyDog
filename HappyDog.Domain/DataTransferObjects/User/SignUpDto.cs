using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HappyDog.Domain.DataTransferObjects.User
{
    public class SignUpDto : IValidatableObject
    {
        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "用户名长度4-16位")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "密码")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "密码长度8-16位")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请确认密码")]
        [Display(Name = "确认密码")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "密码长度8-16位")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "请输入邮箱")]
        [EmailAddress]
        [Display(Name = "邮箱")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "邮箱长度8-16位")]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("密码不一致", new[] { nameof(Password), nameof(ConfirmPassword) });
            }
            if (!Regex.IsMatch(UserName, @"^[a-zA-Z]+\w+$"))
            {
                yield return new ValidationResult("用户名英文字母开头，只能含有：英文、数字、下划线。", new[] { nameof(UserName) });
            }
        }
    }
}
