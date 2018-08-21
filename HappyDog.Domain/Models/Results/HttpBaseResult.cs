using HappyDog.Domain.Enums;

namespace HappyDog.Domain.Models.Results
{
    public class HttpBaseResult
    {
        public NoticeMode NoticeMode { get; set; }
        public string Message { get; set; }
    }
}
