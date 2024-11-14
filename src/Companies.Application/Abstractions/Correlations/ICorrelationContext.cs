namespace Companies.Application.Abstractions.Correlations;

public interface ICorrelationContext
{
    public string CorrelationId { get; }

    public void SetCorrelationId(string correlationId);
}
