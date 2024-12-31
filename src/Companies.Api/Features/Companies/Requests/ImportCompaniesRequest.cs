using Companies.Application.Features.Companies.Commands.ImportCompanies;

namespace Companies.Api.Features.Companies.Requests;

public record ImportCompaniesRequest(CompanyToBeImported[] Companies);