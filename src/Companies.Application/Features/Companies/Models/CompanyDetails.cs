using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies.Enums;

namespace Companies.Application.Features.Companies.Models;

public record CompanyDetails(
    Guid Id, string Cnpj, string Name, CompanyLegalNatureType LegalNature, int MainActivityId,
    AddressModel Address, DateTime CreatedAt, DateTime UpdatedAt, IEnumerable<CompanyPhoneModel> Phones)
{
    public static CompanyDetails FromCompany(Company company) => new(
        company.Id,
        company.Cnpj.Number,
        company.Name,
        company.LegalNature,
        company.MainActivityId,
        AddressModel.FromAddress(company.Address),
        company.CreatedAt,
        company.UpdatedAt,
        company.Phones.Select(CompanyPhoneModel.FromCompanyPhone)
        );
}
