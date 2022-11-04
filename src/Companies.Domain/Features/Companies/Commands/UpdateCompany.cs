using Companies.Domain.Base.Models;
using Companies.Domain.Features.Companies.Enums;

using MediatR;

namespace Companies.Domain.Features.Companies.Commands;

public class UpdateCompany : IRequest<Response>
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = default!;
    public CompanyLegalNatureType LegalNature { get; set; }
    public int MainActivityId { get; set; }
    public AddressModel Address { get; set; } = default!;
    public IEnumerable<PhoneModel> Phones { get; set; } = default!;
}
