using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.Companies.Models;

namespace Companies.Application.Features.Companies.Commands.ImportCompanies;

public record ImportCompaniesCommand(CompanyToBeImported[] Companies) : ICommand;

public record CompanyToBeImported(
    string Cnpj,
    string Name,
    CompanyLegalNatureType LegalNature,
    int MainActivityId,
    AddressModel Address,
    CompanyPartnerModel[] Partners,
    PhoneModel[] Phones);
