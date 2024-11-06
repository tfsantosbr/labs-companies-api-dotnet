using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.Companies.Models;

namespace Companies.Application.Features.Companies.Models;

public class CompanyDetails
{
    public Guid Id { get; set; }
    public string Cnpj { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public CompanyLegalNatureType LegalNature { get; private set; }
    public int MainActivityId { get; private set; }
    public AddressModel Address { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IEnumerable<CompanyPartnerModel> Partners { get; set; } = default!;
    public IEnumerable<CompanyPhoneModel> Phones { get; set; } = default!;
}
