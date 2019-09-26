using RabbitMQ.Client;
using System.Text;

namespace Galaxy.Taurus.MQUtil
{
    public class Producer
    {
        private static object padlock = new object();
        private static IConnection conn;

        private static IConnection GetConnection()
        {
            if (conn == null)
            {
                lock (padlock)
                {
                    if (conn == null)
                        conn = RabbitMQConnectionFactory.CreateConnection();
                }
            }

            return conn;
        }

        public void Publish(string topic, string message)
        {
            IConnection conn = Producer.GetConnection();

            using (IModel channel = conn.CreateModel())
            {
                string queueName = $"{topic}.qu";
                channel.QueueDeclare(queueName, false, false, false, null);
                var msgBody = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", queueName, null, msgBody);
            }
        }
    }
}
