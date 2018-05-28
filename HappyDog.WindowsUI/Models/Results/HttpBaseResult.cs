using HappyDog.WindowsUI.Enums;

namespace HappyDog.WindowsUI.Models.Results
{
    public class HttpBaseResult
    {
        public CodeResult Code { get; set; }
        public NotifyResult Notify { get; set; }
        public string Message { get; set; }
    }
}
