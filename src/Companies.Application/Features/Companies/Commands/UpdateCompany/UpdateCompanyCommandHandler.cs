using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.Validations;
using Companies.Application.Abstractions.ValueObjects;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandHandler(
    ICommandValidator<UpdateCompanyCommand> validator, ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    : AbstractHandler, ICommandHandler<UpdateCompanyCommand>
{
    // Implementations

    public async Task<Result> HandleAsync(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);

        if (validationResult.IsFailure)
            return ErrorResult(validationResult.Notifications);

        if (await IsDuplicatedName(command.Name, command.CompanyId))
            return ErrorResult(CompanyErrors.CompanyNameAlreadyExists(command.Name));

        var company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company == null)
            return NotFoundResult(CompanyErrors.CompanyNotFound(command.CompanyId));

        var updateCompanyResult = UpdateCompany(command, company);

        if (updateCompanyResult.IsFailure)
            return ErrorResult(updateCompanyResult.Notifications);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    // Private Methods

    private static Result<Company> UpdateCompany(UpdateCompanyCommand command, Company company)
    {
        company.Update(
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

        company.ClearPhones();

        foreach (var phone in command.Phones)
        {
            var addPhoneResult = company.AddPhone(new Phone(phone.CountryCode, phone.Number));

            if (addPhoneResult.IsFailure)
                return Result<Company>.Error(addPhoneResult.Notifications);
        }

        return Result<Company>.Success(company);
    }

    private async Task<bool> IsDuplicatedName(string name, Guid ignoreId)
    {
        return await companyRepository.AnyByNameAsync(name, ignoreId);
    }
}
