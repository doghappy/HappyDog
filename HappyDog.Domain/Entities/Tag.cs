using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDog.Domain.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Color { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string GlyphFont { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Glyph { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }
    }
}
