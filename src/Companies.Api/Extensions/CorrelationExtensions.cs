using Companies.Application.Abstractions.Correlations;

namespace Companies.Api.Extensions;

public static class CorrelationExtensions
{
    public static IServiceCollection AddCorrelation(this IServiceCollection services)
    {
        services.AddScoped<ICorrelationContext, CorrelationContext>();

        return services;
    }

    public static IApplicationBuilder UseCorrelationContext(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<CorrelationContext>>();
            var correlationContext = context.RequestServices.GetRequiredService<ICorrelationContext>();
            var correlationId = correlationContext.CorrelationId;

            context.Response.Headers.Append(CorrelationHeaders.ExternalCorrelationHeader, correlationId);

            using (logger.BeginScope(CorrelationScopeFields(CorrelationHeaders.InternalHeaderName, correlationId)))
            {
                await next(context);
            }
        });

        return app;
    }

    private static Dictionary<string, object> CorrelationScopeFields(string correlationHeader, string correlationId) =>
        new() { [correlationHeader] = correlationId };
}
