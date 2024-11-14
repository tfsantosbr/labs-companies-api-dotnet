namespace Companies.Application.Abstractions.Correlations;

public static class CorrelationExtensions
{
    public static IDictionary<string, object> ToHeaders(this ICorrelationContext correlationContext)
    {
        return new Dictionary<string, object>
        {
            [CorrelationHeaders.InternalHeaderName] = correlationContext.CorrelationId
        };
    }
}
