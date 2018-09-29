using MessagesService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessagesService
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private IConnection _connection;
        private IModel _channel;
        private string _replyQueueName;
        private QueueingBasicConsumer _consumer;
        

        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            return factory.CreateConnection();
        }

        public void Close()
        {
            _connection.Close();
        }

        public void Send(IConnection _connection, string friendQueueName, string msg)
        {
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("Chat", ExchangeType.Direct);
            _channel.QueueDeclare(friendQueueName, true, false, false, null);
            _channel.QueueBind(friendQueueName, "Chat", friendQueueName, null);
            var message = Encoding.UTF8.GetBytes(msg);
            _channel.BasicPublish("Chat", friendQueueName, null, message);
        }

        public ReceivedMessage Receive(IConnection _connection, string myqueue)
        {
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(myqueue, true, false, false, null);

            var consumer = new EventingBasicConsumer(_channel);
            var result = _channel.BasicGet(myqueue, true);

            var ret = new ReceivedMessage()
            {
                Message = Encoding.UTF8.GetString(result.Body),
                RoutingKey = result.RoutingKey
            };

            return ret;
        }
    }
}
