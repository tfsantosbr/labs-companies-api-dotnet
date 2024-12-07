using Shared.Application.Domains.DomainEvents;

namespace Shared.Application.Domains;

public abstract class AggregateRoot : IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearEvents() =>
        _domainEvents.Clear();
}
