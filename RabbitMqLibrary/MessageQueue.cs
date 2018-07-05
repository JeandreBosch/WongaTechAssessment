using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqLibrary
{
    public class MessageQueue
    {
        #region Variables
        
        // Constants
        private const string Host = "localhost";

        // Private Fields
        private readonly ConnectionFactory _factory;

        // Public Fields

        #endregion

        #region Constructors

        public MessageQueue()
        {
            _factory = new ConnectionFactory { HostName = Host };
        }

        #endregion

        #region Methods

        public void SendMessage(string queue, string message)
        {
            using (IConnection connection = _factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);

                    byte[] body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", queue, null, body);
                }
            }
        }

        public void ReceiveMessage(string queue, bool autoAck)
        {
            using (IConnection connection = _factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, false, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, eventArgs) =>
                    {
                        byte[] body = eventArgs.Body;
                        string name = Encoding.UTF8.GetString(body);

                        Console.WriteLine($"Message received: {name}");
                        Console.WriteLine();
                    };

                    channel.BasicConsume(queue, autoAck, consumer);

                    Console.ReadLine();
                }
            }
        }

        #endregion
    }
}
