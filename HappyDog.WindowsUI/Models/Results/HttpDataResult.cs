namespace HappyDog.WindowsUI.Models.Results
{
    public class HttpDataResult<T> : HttpBaseResult
    {
        public T Data { get; set; }
    }
}
