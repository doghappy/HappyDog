using System;
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
        [Column(TypeName = "varchar(50)")]
        public string PasswordHash { get; set; }

        public Guid SecurityStamp { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(50)")]
        public string NormalizedEmail { get; set; }

        public int AccessFailedCount { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
