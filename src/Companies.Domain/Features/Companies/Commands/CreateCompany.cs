
using Companies.Domain.Base.Models;
using Companies.Domain.Features.Companies.Enums;
using Companies.Domain.Features.Companies.Models;
using MediatR;

namespace Companies.Domain.Features.Companies.Commands;

public class CreateCompany : IRequest<Response<Company>>
{
    public string Cnpj { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public CompanyLegalNatureType LegalNature { get; private set; }
    public int MainActivityId { get; private set; }
    public AddressModel Address { get; private set; } = default!;
    public IEnumerable<CompanyPartnerModel> Partners { get; set; } = default!;
    public IEnumerable<PhoneModel> Phones { get; set; } = default!;
}