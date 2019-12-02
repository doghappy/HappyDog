using HappyDog.Domain.Enums;

namespace HappyDog.Domain.DataTransferObjects.Category
{
    public class CategoryDto
    {
        public ArticleCategory Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Color { get; set; }
    }
}
