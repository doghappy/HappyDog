using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.User
{
    public class SignInDto
    {
        [Required]
        [StringLength(12)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(16)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}
