using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.User
{
    public class LoginDto
    {
        [Required]
        [StringLength(12)]
        public string UserName { get; set; }

        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
