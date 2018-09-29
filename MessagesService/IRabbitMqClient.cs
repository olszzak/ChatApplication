using MessagesService.Models;
using RabbitMQ.Client;

namespace MessagesService
{
    public interface IRabbitMqClient
    {
        void Close();
        IConnection CreateConnection();
        ReceivedMessage Receive(IConnection _connection, string myqueue);
        void Send(IConnection _connection, string friendQueueName, string msg);
    }
}