using Companies.Application.Base.Models;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.Companies.Models;

namespace Companies.Application.Features.Companies.Commands;

public class ImportCompanies
{
    public IEnumerable<CompanyToBeImported> Companies { get; set; } = null!;
}

public class CompanyToBeImported
{
    public string Cnpj { get; set; } = default!;
    public string Name { get; set; } = default!;
    public CompanyLegalNatureType LegalNature { get; set; }
    public int MainActivityId { get; set; }
    public AddressModel Address { get; set; } = default!;
    public IEnumerable<CompanyPartnerModel> Partners { get; set; } = default!;
    public IEnumerable<PhoneModel> Phones { get; set; } = default!;
}
