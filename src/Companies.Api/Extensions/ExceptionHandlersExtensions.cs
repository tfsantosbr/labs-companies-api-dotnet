using Companies.Api.Extensions.Handlers;

namespace Companies.Api.Extensions;

public static class ExceptionHandlersExtensions
{
    public static IServiceCollection ConfigureExceptionHandlers(this IServiceCollection services, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            return services;

        services.AddExceptionHandler<InternalExceptionHandler>();
        services.AddExceptionHandler<BadHttpRequestExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
