namespace Companies.Application.Abstractions.Correlations;

public class CorrelationContext : ICorrelationContext
{
    public string CorrelationId { get; private set; } = Guid.NewGuid().ToString();

    public void SetCorrelationId(string correlationId)
    {
        CorrelationId = correlationId;
    }
}
