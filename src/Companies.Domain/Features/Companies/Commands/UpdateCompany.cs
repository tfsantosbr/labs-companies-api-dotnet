using System.Text.Json.Serialization;

using Companies.Domain.Base.Models;
using Companies.Domain.Features.Companies.Enums;

namespace Companies.Domain.Features.Companies.Commands;

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
