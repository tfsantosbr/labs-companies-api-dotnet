using Companies.Application.Abstractions.Domains;

namespace Companies.Application.Features.Companies.Events;

public record CompanyPartnerAddedDomainEvent(Guid CompanyId, Guid PartnerId) : DomainEvent;
