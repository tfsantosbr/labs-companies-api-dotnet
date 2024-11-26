using Companies.Application.Abstractions.Correlations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Extensions.Handlers;

public class InternalExceptionHandler(ILogger<InternalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is BadHttpRequestException)
            return false;

        var correlationContext = httpContext.RequestServices.GetRequiredService<ICorrelationContext>();
        var correlationId = correlationContext.CorrelationId;

        using (logger.BeginScope(PushFields(CorrelationHeaders.InternalHeaderName, correlationId)))
        {
            logger.LogError(exception, "An unhandled exception has occurred: {ExceptionMessage}", exception.Message);
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.Headers.Append(CorrelationHeaders.ExternalCorrelationHeader, correlationId);

        var errorDetails = new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred on the server.",
            Extensions = new Dictionary<string, object?>
            {
                [CorrelationHeaders.ExternalCorrelationHeader.ToLowerInvariant()] = correlationId,
            }
        };

        await httpContext.Response.WriteAsJsonAsync(errorDetails, cancellationToken);

        return true;
    }

    private static Dictionary<string, object> PushFields(string correlationHeader, string correlationId) =>
        new() { [correlationHeader] = correlationId };
}
