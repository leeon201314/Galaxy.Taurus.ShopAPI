namespace Galaxy.Taurus.FileAPI.Entitys
{
    /// <summary>
    /// 辅助类，数据库不存在
    /// </summary>
    public class DataResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }

    public class DataResultCode
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
    }
}
