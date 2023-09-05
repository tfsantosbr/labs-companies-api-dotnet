using Companies.Import.Worker.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ImportCompanyConsumer>();
    })
    .Build();

host.Run();
