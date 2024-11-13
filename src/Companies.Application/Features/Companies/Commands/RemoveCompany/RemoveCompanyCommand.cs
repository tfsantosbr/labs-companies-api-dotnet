using Companies.Application.Abstractions.Handlers;

namespace Companies.Application.Features.Companies.Commands.RemoveCompany;

public record RemoveCompanyCommand(Guid CompanyId) : ICommand;
