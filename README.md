# WongaTechAssessment

RabbitMQ Console Application that exposes a helper library that can add Messages to the RabbitMQ Queues and Receive messages in two different Console Applications (Producer App and Consumer App).

## Getting Started

These instructions will help you get started in running my applications.

### Installing

Follow the following steps to get started

Run the Receiver (Consumer) Console application first by opening the following batch file:

```
Run Receiver Client.bat
```

Then, run the Sender (Producer) Console application by opening the following batch file:

```
Run Sender Client.bat
```

Once you see the Sender (Producer) Console application's window, do the following:
* Enter a few names in the Sender (Producer) Console application's window.
* Messages will be received in Receiver (Consumer) Console application's window.

## Built With

* [.NET Core & C#](https://www.microsoft.com/net/learn/get-started/windows) - The coding language and framework that was used to build these apps.
* [RabbitMQ](https://www.rabbitmq.com/#getstarted) - The Messaging Broker service that I used.
