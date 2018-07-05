using System;
using System.Collections.ObjectModel;
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
        public ObservableCollection<string> MessagesCollection { get; }

        #endregion

        #region Constructors

        public MessageQueue()
        {
            // Create a new connection to the RabbitMQ Service when this class is initialized.
            _factory = new ConnectionFactory { HostName = Host };

            // Also instantiate a new instance of this collection so it is ready for use.
            MessagesCollection = new ObservableCollection<string>();
        }

        #endregion

        #region Methods

        public void SendMessage(string queue, string message)
        {
            // Open a new connection to the RabbitMQ Service.
            using (IConnection connection = _factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    // Declare the Queue if it does not already exist.
                    channel.QueueDeclare(queue, false, false, false, null);

                    // Create a byte array from the passed string message.
                    byte[] body = Encoding.UTF8.GetBytes(message);

                    // Publish the Message to the specified Queue.
                    channel.BasicPublish("", queue, null, body);
                }
            }
        }

        public void ReceiveMessage(string queue, bool autoAck)
        {
            // Open a new connection to the RabbitMQ Service.
            using (IConnection connection = _factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    // Declare the Queue if it does not already exist.
                    channel.QueueDeclare(queue, false, false, false, null);

                    // Register callback event to add to Observable collection when new Message is received from the Queue.
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, eventArgs) =>
                    {
                        byte[] body = eventArgs.Body;
                        string name = Encoding.UTF8.GetString(body);

                        // Add to Observable list so the client can subscribe to collection changed events.
                        MessagesCollection.Add($"Message received: {name}");
                    };

                    channel.BasicConsume(queue, autoAck, consumer);

                    // Not sure why this is needed, but breaks without it.
                    Console.ReadLine();
                }
            }
        }

        #endregion
    }
}
