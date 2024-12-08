using Companies.Application.Abstractions.Domains;

namespace Companies.Application.Features.Companies.Events;

public record CompanyUpdatedDomainEvent(Guid CompanyId, string CompanyName) : DomainEvent;
