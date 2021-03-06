﻿using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
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
                messageQueue.MessagesCollection.CollectionChanged += MessagesCollection_CollectionChanged;
                messageQueue.ReceiveMessage(MessageQueueName, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void MessagesCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // For all new items added to the Observable collection, write to the console window.
            IList messages = e.NewItems;
            foreach (string message in messages)
            {
                Console.WriteLine(message);

                string receivedName = message.Split(' ').Last();
                Console.WriteLine($"Hello {receivedName}, I am your father!");

                Console.WriteLine();
            }
        }
    }
}
