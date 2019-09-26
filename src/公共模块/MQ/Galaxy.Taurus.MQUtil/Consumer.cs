using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Galaxy.Taurus.MQUtil
{
    public class Consumer
    {
        public void Received(string topic, Action<string> handler)
        {
            IConnection conn = RabbitMQConnectionFactory.CreateConnection();
            IModel channel = conn.CreateModel();

            string queueName = $"{topic}.qu";
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var msg = Encoding.UTF8.GetString(ea.Body);

                handler?.Invoke(msg);

                //处理完成，告诉Broker可以服务端可以删除消息，分配新的消息过来
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            //noAck设置false,告诉broker，发送消息之后，消息暂时不要删除，等消费者处理完成再说
            channel.BasicConsume(queueName, autoAck: false, consumer: consumer);
        }
    }
}
