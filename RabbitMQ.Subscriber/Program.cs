﻿// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://oqunpwrq:1x7uT2QE4Orp0UJZCSR7ArLHg5XIjhqR@woodpecker.rmq.cloudamqp.com/oqunpwrq");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

//channel.QueueDeclare("hello-queue", true, false, false);

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("hello-queue", false, consumer); //here if we set the true, it`s gonna make queue message dissappear right after it sends it to the consumer.

consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    // Thread.Sleep(1500);

    Console.WriteLine("The income message:" + message);

    channel.BasicAck(e.DeliveryTag, false);
};
Console.ReadLine();
