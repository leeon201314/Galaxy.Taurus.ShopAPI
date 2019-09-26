namespace Galaxy.Taurus.AuthorizationAPI.ViewModel
{
    public class MessageViewModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }

    public class MessageCode
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
}
