namespace Galaxy.Taurus.ShopInfoAPI.ViewModel
{
    public class ResultViewModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }

    public class ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// 失败
        /// </summary>
        public const int Fail = 0;
    }
}
