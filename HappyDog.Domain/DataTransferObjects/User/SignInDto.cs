using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.User
{
    public class SignInDto
    {
        [Required(ErrorMessage = "请输入用户名")]
        [StringLength(12, ErrorMessage = "用户名最长12位")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(16, ErrorMessage = "密码最长16位")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}
