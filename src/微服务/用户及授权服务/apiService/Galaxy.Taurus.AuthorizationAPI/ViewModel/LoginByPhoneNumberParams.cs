namespace Galaxy.Taurus.AuthorizationAPI.ViewModel
{
    /// <summary>
    /// 使用手机号时的登录参数
    /// </summary>
    public class LoginByPhoneNumberParams
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
