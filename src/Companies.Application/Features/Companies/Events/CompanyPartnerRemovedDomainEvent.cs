using Companies.Application.Abstractions.Domains;

namespace Companies.Application.Features.Companies.Events;

public record CompanyPartnerRemovedDomainEvent(Guid CompanyId, Guid PartnerId) : DomainEvent;
