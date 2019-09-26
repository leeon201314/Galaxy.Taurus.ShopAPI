using AutoMapper;
using Galaxy.Taurus.GoodInfoAPI.Entitys;

namespace Galaxy.Taurus.GoodInfoAPI.ViewModel
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class ResultViewModel
    {
        /// <summary>
        /// 结果码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }

    /// <summary>
    /// 操作结果码
    /// </summary>
    public class ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// 参数错误
        /// </summary>
        public const int ParamsError = 301;

        /// <summary>
        /// 失败
        /// </summary>
        public const int Fail = 400;

        /// <summary>
        /// 未登录
        /// </summary>
        public const int NoLogin = 401;
    }

    /// <summary>
    /// 操作结果消息
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const string Success = "success";
    }
}
