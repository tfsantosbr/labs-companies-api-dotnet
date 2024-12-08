using Companies.Application.Abstractions.Domains;

namespace Companies.Application.Features.Companies.Events;

public record CompanyCreatedDomainEvent(Guid CompanyId, string CompanyName) : DomainEvent;
