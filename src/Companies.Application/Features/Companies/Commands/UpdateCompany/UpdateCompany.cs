using System.Text.Json.Serialization;

using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies.Enums;

namespace Companies.Application.Features.Companies.Commands.UpdateCompany;

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
