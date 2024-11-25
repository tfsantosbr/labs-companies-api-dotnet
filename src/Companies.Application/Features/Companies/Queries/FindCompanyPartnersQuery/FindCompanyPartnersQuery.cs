using Companies.Application.Abstractions.Handlers;
using Companies.Application.Features.Partners.Models;

namespace Companies.Application.Features.Companies.Queries.FindCompanyPartnersQuery;

public record FindCompanyPartnersQuery(Guid CompanyId) : IQuery<IEnumerable<PartnerItem>>;
