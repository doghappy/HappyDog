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
        [MaxLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string Password { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
