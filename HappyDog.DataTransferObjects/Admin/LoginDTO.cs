using System.ComponentModel.DataAnnotations;

namespace HappyDog.DataTransferObjects.Admin
{
    public class LoginDTO
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
