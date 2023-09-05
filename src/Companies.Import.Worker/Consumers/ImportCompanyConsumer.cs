
namespace Companies.Import.Worker.Consumers;

public class ImportCompanyConsumer : BackgroundService
{
    private readonly ILogger<ImportCompanyConsumer> _logger;

    public ImportCompanyConsumer(ILogger<ImportCompanyConsumer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Import Company Consumer running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
