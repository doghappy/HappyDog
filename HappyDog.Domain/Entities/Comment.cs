using HappyDog.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDog.Domain.Entities
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string IPv4 { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTimeOffset CreateTime { get; set; }

        public BaseStatus Status { get; set; }
    }
}
