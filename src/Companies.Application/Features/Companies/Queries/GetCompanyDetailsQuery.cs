using Companies.Application.Abstractions.Handlers;
using Companies.Application.Features.Companies.Models;

namespace Companies.Application.Features.Companies.Queries;

public record GetCompanyDetailsQuery(Guid CompanyId) : IQuery<CompanyDetails>;
