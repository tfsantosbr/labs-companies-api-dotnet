using Companies.Domain.Base.Models;

using MediatR;

namespace Companies.Domain.Features.Companies.Commands;

public class RemoveCompanyPartner : IRequest<Response>
{
    public Guid CompanyId { get; set; }
    public Guid PartnerId { get; set; }
}
