
using System.Text;
using System.Text.Json;

using Companies.Domain.Features.Companies.Commands;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Companies.Import.Worker.Consumers;

public class ImportCompanyConsumer : BackgroundService
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly string _querueName = "import-companies-queue";
    private readonly ILogger<ImportCompanyConsumer> _logger;

    public ImportCompanyConsumer(ILogger<ImportCompanyConsumer> logger)
    {
        _logger = logger;

        _connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var createCompany = JsonSerializer.Deserialize<CreateCompany>(contentString);

            if (createCompany is null)
                return;

            _logger.LogInformation(
                "Received message: CreateCompany with CNPJ: {cnpj}", 
                createCompany.Cnpj
                );

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        channel.BasicConsume(_querueName, false, consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
