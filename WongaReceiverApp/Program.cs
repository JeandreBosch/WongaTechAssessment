using System;
using RabbitMqLibrary;

namespace WongaReceiverApp
{
    public class Program
    {
        private const string MessageQueueName = "WongaServiceBusApp";

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Press [Enter] to exit application...");
                Console.WriteLine();

                var messageQueue = new MessageQueue();
                messageQueue.ReceiveMessage(MessageQueueName, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
