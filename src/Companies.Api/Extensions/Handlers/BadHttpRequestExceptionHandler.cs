using System.Text.Json;
using Companies.Application.Abstractions.Correlations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Extensions.Handlers;

public class BadHttpRequestExceptionHandler(ILogger<BadHttpRequestExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not BadHttpRequestException badRequestException)
            return false;

        var correlationContext = httpContext.RequestServices.GetRequiredService<ICorrelationContext>();
        var correlationId = correlationContext.CorrelationId;

        logger.LogWarning(badRequestException, "An unhandled bad request exception has occurred: {Message}",
            badRequestException.Message);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.Headers.Append(CorrelationHeaders.ExternalCorrelationHeader, correlationId);

        var innerException = badRequestException.InnerException as JsonException;

        var problemDetails = new ProblemDetails
        {
            Title = "Bad Request",
            Detail = badRequestException.Message,
            Extensions = new Dictionary<string, object?>
            {
                ["field"] = innerException?.Path,
            }
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
