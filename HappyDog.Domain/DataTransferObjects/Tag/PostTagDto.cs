using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.Tag
{
    public class PostTagDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Color { get; set; }

        [MaxLength(100)]
        public string GlyphFont { get; set; }

        [MaxLength(10)]
        public string Glyph { get; set; }
    }
}
