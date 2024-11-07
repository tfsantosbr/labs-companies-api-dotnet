using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.Companies.Models;

namespace Companies.Api.Models.Companies;

public record CreateCompanyRequest(
    string Cnpj,
    string Name,
    CompanyLegalNatureType LegalNature,
    int MainActivityId,
    AddressModel Address,
    IEnumerable<CompanyPartnerModel> Partners,
    IEnumerable<PhoneModel> Phones) : ICommand<CompanyDetails>;
