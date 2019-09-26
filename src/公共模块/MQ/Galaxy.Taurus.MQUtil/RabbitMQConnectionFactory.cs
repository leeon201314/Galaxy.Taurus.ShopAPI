using RabbitMQ.Client;
using System;

namespace Galaxy.Taurus.MQUtil
{
    /// <summary>
    /// RabbitMQ连接工厂
    /// </summary>
    public class RabbitMQConnectionFactory
    {
        private static object padLock = new object();
        private static ConnectionFactory rabbitMqFactory;

        public static IConnection CreateConnection()
        {
            if (rabbitMqFactory == null)
                lock (padLock)
                    if (rabbitMqFactory == null)
                    {
                        if (string.IsNullOrEmpty(RabbitMQConfigInfo.HostName))
                        {
                            throw new Exception("Joyo.Common.MQUtil：未对RabbitMQ的连接信息进行配置");
                        }

                        rabbitMqFactory = new ConnectionFactory();
                        rabbitMqFactory.UserName = RabbitMQConfigInfo.UserName;   //用户名
                        rabbitMqFactory.Password = RabbitMQConfigInfo.Password;      //密码
                        rabbitMqFactory.HostName = RabbitMQConfigInfo.HostName;  //Rabbitmq服务IP，不包含端口
                        rabbitMqFactory.VirtualHost = "/";  //默认为"/"
                    }

            return rabbitMqFactory.CreateConnection();
        }
    }
}
