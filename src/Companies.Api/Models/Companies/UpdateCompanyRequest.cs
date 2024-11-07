using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies.Enums;

namespace Companies.Api.Models.Companies;

public record UpdateCompanyRequest(
    string Name,
    CompanyLegalNatureType LegalNature,
    int MainActivityId,
    AddressModel Address,
    IEnumerable<PhoneModel> Phones
    );
