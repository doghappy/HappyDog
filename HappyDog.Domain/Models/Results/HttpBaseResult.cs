using System;
using HappyDog.Domain.Enums;

namespace HappyDog.Domain.Models.Results
{
    /// <summary>
    /// 基于Http状态码的基本模型
    /// </summary>
    public class HttpBaseResult
    {
        /// <summary>
        /// 基于Http状态码的基本模型
        /// </summary>
        public HttpBaseResult() { }

        /// <summary>
        /// 基于Http状态码的基本模型
        /// </summary>
        /// <param name="autoMsg">是否根据Code自动设置消息</param>
        public HttpBaseResult(bool autoMsg)
        {
            this.autoMsg = autoMsg;
        }

        protected bool autoMsg;

        public CodeResult Code { get; set; }
        public NotifyResult Notify { get; set; }

        private string message;
        public string Message
        {
            get => autoMsg ? GetAutoMessage() : message;
            set
            {
                if (!autoMsg)
                {
                    message = value;
                }
            }
        }

        public string GetAutoMessage()
        {
            switch (Code)
            {
                case CodeResult.OK:
                    return "请求已成功，请求所希望的响应头或数据体将随此响应返回。";
                case CodeResult.BadRequest:
                    return "请求参数有误。";
                case CodeResult.Unauthorized:
                    return "当前请求需要用户验证。";
                case CodeResult.Forbidden:
                    return "服务器已经理解请求，但是拒绝执行它。";
                case CodeResult.InternalServerError:
                    return "服务器遇到了一个未曾预料的状况，导致了它无法完成对请求的处理。";
                case CodeResult.NotImplemented:
                    return "服务器不支持当前请求所需要的某个功能。当服务器无法识别请求的方法，并且无法支持其对任何资源的请求。";
                default:
                    throw new NotImplementedException();
            }
        }

        public static HttpBaseResult NotImplemented => new HttpBaseResult(true)
        {
            Code = CodeResult.NotImplemented,
            Notify = NotifyResult.Danger
        };

        public static HttpBaseResult InternalServerError => new HttpBaseResult(true)
        {
            Code = CodeResult.InternalServerError,
            Notify = NotifyResult.Danger
        };

        public static HttpBaseResult Unauthorized => new HttpBaseResult(true)
        {
            Code = CodeResult.Unauthorized,
            Notify = NotifyResult.Warning
        };
    }
}
