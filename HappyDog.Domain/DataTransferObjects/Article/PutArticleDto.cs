using HappyDog.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HappyDog.Domain.DataTransferObjects.Article
{
    public class PutArticleDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public BaseState State { get; set; }
    }
}
