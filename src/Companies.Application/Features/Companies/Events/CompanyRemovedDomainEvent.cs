using Companies.Application.Abstractions.Domains;

namespace Companies.Application.Features.Companies.Events;

public record CompanyRemovedDomainEvent(Guid CompanyId, string CompanyName) : DomainEvent;
