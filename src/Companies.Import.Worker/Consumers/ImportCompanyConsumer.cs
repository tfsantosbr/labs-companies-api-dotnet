
using System.Text;
using System.Text.Json;

using Companies.Application.Abstractions.Handlers;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Models;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Companies.Import.Worker.Consumers;

public class ImportCompanyConsumer : BackgroundService
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly string _querueName = "import-companies-queue";
    private readonly ILogger<ImportCompanyConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;


    public ImportCompanyConsumer(
        ILogger<ImportCompanyConsumer> logger,
        IConfiguration configuration,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        _connectionFactory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Host"],
            UserName = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
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

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var createCompany = JsonSerializer.Deserialize<CreateCompanyCommand>(contentString);

            if (createCompany is null)
                return;

            _logger.LogInformation(
                "Received message: CreateCompanyCommand with CNPJ: {cnpj}",
                createCompany.Cnpj
                );

            using (var scope = _serviceProvider.CreateScope())
            {
                var createCompanyHandler = scope.ServiceProvider
                    .GetRequiredService<ICommandHandler<CreateCompanyCommand, CompanyDetails>>();

                var result = await createCompanyHandler.HandleAsync(createCompany);

                if (result.IsFailure)
                {
                    _logger.LogWarning(
                        "Error while create company with CNPJ {cnpj}: \n {errors}",
                        createCompany.Cnpj,
                        JsonSerializer.Serialize(result.Notifications)
                        );
                }
                else
                {
                    _logger.LogInformation(
                        "Company with CNPJ {cnpj} created with success. Id: {id}",
                        createCompany.Cnpj,
                        result.Data!.Id
                        );
                }
            }

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        channel.BasicConsume(_querueName, false, consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
