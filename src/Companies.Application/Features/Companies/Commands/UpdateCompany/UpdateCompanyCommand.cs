
using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies.Enums;

namespace Companies.Application.Features.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand(
    Guid CompanyId, 
    string Name, 
    CompanyLegalNatureType LegalNature, 
    int MainActivityId, 
    AddressModel Address, 
    IEnumerable<PhoneModel> Phones
    ) : ICommand;
