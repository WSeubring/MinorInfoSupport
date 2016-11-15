using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class MessageReceiver
{
    private IModel _channel;

    public MessageReceiver(IModel channel)
    {
        _channel = channel;

        channel.ExchangeDeclare(exchange: "messages", type: "fanout");

        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName,
                          exchange: "messages",
                          routingKey: "");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" Message: {0}", message);
        };

        channel.BasicConsume(queue: queueName,
                             noAck: true,
                             consumer: consumer);
    }
}