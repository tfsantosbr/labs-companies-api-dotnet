using Companies.Api.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Companies.Api.Extensions;

public static class HealthCheckExtensions
{
    public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("Postgres") ??
            throw new NullReferenceException("Postgres");

        services.AddHealthChecks()
            .AddNpgSql(postgresConnectionString, tags: ["ready"]);
    }

    public static void UseHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = healthCheck => healthCheck.Tags.Contains("ready")
        });

        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.MapHealthChecks("/health/details", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

    }
}
