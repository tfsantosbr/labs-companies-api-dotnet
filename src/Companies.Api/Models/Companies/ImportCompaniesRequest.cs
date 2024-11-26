using Companies.Application.Features.Companies.Commands.ImportCompanies;

namespace Companies.Api.Models.Companies;

public record ImportCompaniesRequest(CompanyToBeImported[] Companies);