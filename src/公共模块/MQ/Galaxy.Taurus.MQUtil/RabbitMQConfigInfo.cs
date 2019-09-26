namespace Galaxy.Taurus.MQUtil
{
    public class RabbitMQConfigInfo
    {
        /// <summary>
        /// Rabbitmq服务IP，不包含端口
        /// </summary>
        public static string HostName { get; private set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public static string Password { get; private set; }

        public static void Config(string hostName, string userName, string password)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
        }
    }
}
