using Companies.Api.Extensions;

using Serilog;

namespace Companies.Api.Extensions;

public static class SerilogExtensions
{
    public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((services, logger) => logger
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
        );

        return services;
    }
}
