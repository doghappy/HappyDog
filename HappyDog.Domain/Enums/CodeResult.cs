namespace HappyDog.Domain.Enums
{
    public enum CodeResult
    {
        /// <summary>
        /// 请求已成功，请求所希望的响应头或数据体将随此响应返回。
        /// </summary>
        OK = 200,

        /// <summary>
        /// 请求参数有误。
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// 当前请求需要用户验证。
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// 服务器已经理解请求，但是拒绝执行它。
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// 所请求的资源不在服务器上。
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 服务器遇到了一个未曾预料的状况，导致了它无法完成对请求的处理。
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// 服务器不支持所请求的功能。
        /// </summary>
        NotImplemented = 501
    }
}
