using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.Validations;
using Companies.Application.Abstractions.ValueObjects;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Models;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Commands.CreateCompany;

public class CreateCompanyCommandHandler(
    ICommandValidator<CreateCompanyCommand> validator,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : AbstractHandler<CompanyDetails>, ICommandHandler<CreateCompanyCommand, CompanyDetails>
{
    // Implementations

    public async Task<Result<CompanyDetails>> HandleAsync(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var result = validator.Validate(command);

        if (result.IsFailure)
            return ErrorResult(result.Notifications);

        if (await IsDuplicatedCnpj(command.Cnpj))
            return ErrorResult(CompanyErrors.CompanyCnpjAlreadyExists(command.Cnpj));

        if (await IsDuplicatedName(command.Name))
            return ErrorResult(CompanyErrors.CompanyNameAlreadyExists(command.Name));

        var createCompanyResult = CreateCompany(command);

        if (createCompanyResult.IsFailure)
            return ErrorResult(createCompanyResult.Notifications);

        var company = createCompanyResult.Data!;

        await companyRepository.AddAsync(company, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return SuccessResult(CompanyDetails.FromCompany(company));
    }

    // Private Methods

    private static Result<Company> CreateCompany(CreateCompanyCommand command)
    {
        var result = Company.Create(
            cnpj: new Cnpj(command.Cnpj),
            name: command.Name,
            legalNature: command.LegalNature,
            mainActivityId: command.MainActivityId,
            address: new Address(
                postalCode: command.Address.PostalCode,
                street: command.Address.Street,
                number: command.Address.Number,
                complement: command.Address.Complement,
                neighborhood: command.Address.Neighborhood,
                city: command.Address.City,
                state: command.Address.State,
                country: command.Address.Country
            )
        );

        if (!result.IsSuccess)
            return result;

        var company = result.Data!;

        foreach (var partner in command.Partners)
        {
            var addPartnerResult = company.AddPartner(
                partner.PartnerId, partner.QualificationId, DateOnly.FromDateTime(partner.JoinedAt));

            if (addPartnerResult.IsFailure)
                return Result<Company>.Error(addPartnerResult.Notifications);
        }

        foreach (var phone in command.Phones)
        {
            var addPhoneResult = company.AddPhone(new Phone(phone.CountryCode, phone.Number));

            if (addPhoneResult.IsFailure)
                return Result<Company>.Error(addPhoneResult.Notifications);
        }

        return Result<Company>.Success(company);
    }

    private async Task<bool> IsDuplicatedName(string name) =>
        await companyRepository.AnyByNameAsync(name);

    private async Task<bool> IsDuplicatedCnpj(string cnpj) =>
        await companyRepository.AnyByCnpjAsync(cnpj);
}
