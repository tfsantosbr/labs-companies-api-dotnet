using Companies.Application.Abstractions.Handlers;

namespace Companies.Application.Features.Companies.Commands.RemovePartnerFromCompany;

public record RemovePartnerFromCompanyCommand(Guid CompanyId, Guid PartnerId) : ICommand;
