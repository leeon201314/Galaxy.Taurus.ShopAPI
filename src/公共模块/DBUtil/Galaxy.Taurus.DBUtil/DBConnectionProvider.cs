using Galaxy.Taurus.IDBUtil;
using System;

namespace Galaxy.Taurus.DBUtil
{
    public class DBConnectionProvider : IDBConnectionProvider
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public static string DBName { get; private set; }

        /// <summary>
        /// 数据库服务器
        /// </summary>
        public static string Server { get; private set; }

        /// <summary>
        /// 端口
        /// </summary>
        public static int Port { get; private set; }

        /// <summary>
        //用户名
        /// </summary>
        public static string User { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public static string Password { get; private set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="server">数据库服务器</param>
        /// <param name="port">端口</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public static void Config(string server, int port, string dbName, string user, string password)
        {
            Server = server;
            Port = port;
            DBName = dbName;
            User = user;
            Password = password;
        }

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(Server) || string.IsNullOrEmpty(DBName) || string.IsNullOrEmpty(User))
            {
                throw new Exception("错误：数据库连接参数未进行配置");
            }

            return $"server={Server};port={Port};database={DBName};user={User};password={Password};SslMode=None;";
        }
    }
}
