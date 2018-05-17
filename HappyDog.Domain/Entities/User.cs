using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDog.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [StringLength(16)]
        public string Password { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
