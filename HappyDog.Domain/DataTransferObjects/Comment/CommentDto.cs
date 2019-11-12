using System;

namespace HappyDog.Domain.DataTransferObjects.Comment
{
    public class CommentDto
    {
        public string Name { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public string Content { get; set; }
    }
}
