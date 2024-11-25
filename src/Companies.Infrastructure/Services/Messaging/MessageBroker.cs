using System.Text;
using System.Text.Json;
using Companies.Application.Abstractions.Messaging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Companies.Infrastructure.Services.Messaging;

public class MessageBroker : IMessageBroker
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly string _querueName = "import-companies-queue";

    public MessageBroker(IConfiguration configuration)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Host"],
            UserName = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
        };
    }

    public Task PostMessageAsync<TMessage>(TMessage message)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: _querueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var messageAsString = JsonSerializer.Serialize(message);
        var messageInBytes = Encoding.UTF8.GetBytes(messageAsString);

        channel.BasicPublish(
            exchange: "",
            routingKey: _querueName,
            basicProperties: null,
            body: messageInBytes
        );

        return Task.CompletedTask;
    }
}
