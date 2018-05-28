namespace HappyDog.WindowsUI.Models.Results
{
    /// <summary>
    /// 基于Http状态码的模型，并带有一个泛型数据。
    /// </summary>
    public class HttpDataResult<T> : HttpBaseResult
    {
        public T Data { get; set; }
    }
}
