namespace Galaxy.Taurus.OrderingAPI.ViewModel
{
    /// <summary>
    ///结果
    /// </summary>
    public class ResultViewModel<T>
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
        public T Data { get; set; }
    }

    /// <summary>
    /// 结果码
    /// </summary>
    public class ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// 失败
        /// </summary>
        public const int Fail = 400;

        /// <summary>
        /// 未登录
        /// </summary>
        public const int NoLogin = 401;

        /// <summary>
        /// 用户不存在
        /// </summary>
        public const int NoUser = 402;
    }

    /// <summary>
    /// 结果消息
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const string Success = "success";
    }
}
