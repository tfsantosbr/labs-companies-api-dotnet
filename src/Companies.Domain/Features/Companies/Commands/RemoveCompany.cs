using Companies.Domain.Base.Models;

using MediatR;

namespace Companies.Domain.Features.Companies.Commands;

public class RemoveCompany : IRequest<Response>
{
    public RemoveCompany(Guid companyId)
    {
        CompanyId = companyId;
    }

    public Guid CompanyId { get; set; }
}
