using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappyDog.DataTransferObjects.Admin
{
    public class RegisterDTO : IValidatableObject
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("密码不一致");
            }
        }
    }
}
