using HappyDog.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDog.Domain.Entities
{
    [Table("Categories")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Label { get; set; }

        [Required]
        [MaxLength(20)]
        public string Value { get; set; }

        [Required]
        [MaxLength(10)]
        public string Color { get; set; }

        [Required]
        public BaseState State { get; set; }

        public List<Article> Article { get; set; }
    }
}
