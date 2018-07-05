﻿using System;
using RabbitMqLibrary;

namespace WongaSenderApp
{
    public class Program
    {
        private const string MessageQueueName = "WongaServiceBusApp";

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Leave field blank and press [Enter] to close application...");
                Console.WriteLine();

                while (true)
                {
                    Console.Write("Please enter your name: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                        Environment.Exit(0);

                    var messageQueue = new MessageQueue();
                    messageQueue.SendMessage(MessageQueueName, name);

                    Console.WriteLine($"Message sent: {name}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
