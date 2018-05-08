namespace HappyDog.Domain.Models.Results
{
    /// <summary>
    /// 基于Http状态码的模型，并带有一个泛型数据。
    /// </summary>
    public class HttpDataResult<T> : HttpBaseResult
    {
        /// <summary>
        /// 基于Http状态码的模型，并带有一个泛型数据。
        /// </summary>
        public HttpDataResult() : base() { }

        /// <summary>
        /// 基于Http状态码的模型，并带有一个泛型数据。
        /// </summary>
        /// <param name="autoMsg">是否根据Code自动设置消息</param>
        public HttpDataResult(bool autoMsg) : base(autoMsg) { }

        public T Data { get; set; }
    }
}
