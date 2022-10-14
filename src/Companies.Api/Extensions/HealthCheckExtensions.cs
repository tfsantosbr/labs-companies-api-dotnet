namespace Companies.Api.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks();

        return services;
    }

    public static WebApplication UseHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthz");

        return app;
    }
}
