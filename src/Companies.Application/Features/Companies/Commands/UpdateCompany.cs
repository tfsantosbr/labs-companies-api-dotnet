using System.Text.Json.Serialization;

using Companies.Application.Base.Models;
using Companies.Application.Features.Companies.Enums;

namespace Companies.Application.Features.Companies.Commands;

public class UpdateCompany
{
    [JsonIgnore]
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = default!;
    public CompanyLegalNatureType LegalNature { get; set; }
    public int MainActivityId { get; set; }
    public AddressModel Address { get; set; } = default!;
    public IEnumerable<PhoneModel> Phones { get; set; } = default!;
}
