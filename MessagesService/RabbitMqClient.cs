using System;
using MessagesService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessagesService
{
    // TODO BP: ta klasa równie dobrze mogłaby być statyczna. nie przechowujesz w niej połączenia, bo przekazujesz je w parametrze do Send/Receive.
    public class RabbitMqClient : IRabbitMqClient
    {
        // TODO BP: nieużywany var
        private IConnection _connection;
        // TODO BP: A) channel powinieneś ustawiać lokalnie w każdej z metod
        //          B) masz tu potencjalnego memory leaka, IModel implementuje IDisposable, więc powinieneś go używać w usingu
        private IModel _channel;
        // TODO BP: nieużywany var
        private string _replyQueueName;
        private QueueingBasicConsumer _consumer;


        public IConnection CreateConnection()
        {
            // TODO BP: znowu dane logowania trzymasz zahardcodowane
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            return factory.CreateConnection();
        }

        // TODO BP: powinieneś zamiast tego zaimplementować interfejs IDisposable na tej klase
        public void Close()
        {
            _connection.Close();
        }

        // TODO BP: "_connection" dlaczego podkreślnik?
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

            // TODO BP: nieużywany var.
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
