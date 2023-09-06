using Companies.Api.Extensions;

using Serilog;

Log.Logger = ObservabilityExtensions.BuildLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    builder.Services.AddApiServices(configuration);
    builder.Services.AddHealthChecks(configuration);
    builder.Services.AddDatabaseContext(configuration);
    builder.Services.AddApplication();

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
        app.MigrateAndSeedData();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseCors();
    app.MapControllers();
    app.UseHealthChecks();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}