using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;

namespace Companies.Api.Extensions;

public static class ObservabilityExtensions
{
    public static IServiceCollection AddObservability(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }

    public static ReloadableLogger BuildLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }
}


