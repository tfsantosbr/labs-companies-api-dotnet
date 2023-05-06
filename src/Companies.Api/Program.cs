using System.Reflection;
using System.Text.Json.Serialization;
using Companies.Api.Extensions;

using Serilog;
using Serilog.Formatting.Json;

Log.Logger = ObservabilityExtensions.BuildLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    builder.Services.AddCors(setup =>
        setup.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddHealthChecks(configuration);
    builder.Services.AddDatabaseContext(configuration);
    builder.Services.AddApplication();

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File(new JsonFormatter(renderMessage: true), "log.json"));

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
        app.MigrateAndSeedData();

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