using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.Validations;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Models;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Features.Partners.Repositories;

namespace Companies.Application.Features.Companies.Commands.AddPartnerInCompany;

public class AddPartnerInCompanyCommandHandler(
    ICommandValidator<AddPartnerInCompanyCommand> validator, ICompanyRepository companyRepository, IUnitOfWork unitOfWork,
    IPartnerRepository partnerRepository)
    : AbstractHandler<CompanyPartnerModel>, ICommandHandler<AddPartnerInCompanyCommand, CompanyPartnerModel>
{
    public async Task<Result<CompanyPartnerModel>> HandleAsync(AddPartnerInCompanyCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);

        if (validationResult.IsFailure)
            return ErrorResult(validationResult.Notifications);

        var company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company is null)
            return ErrorResult(CompanyErrors.CompanyNotFound(command.CompanyId));

        if (await PartnerNotFound(command.PartnerId))
            return ErrorResult(CompanyErrors.PartnerNotFound(command.PartnerId));

        var addPartnerResult = company.AddPartner(
            command.PartnerId, command.QualificationId, DateOnly.FromDateTime(command.JoinedAt));

        if (addPartnerResult.IsFailure)
            return ErrorResult(addPartnerResult.Notifications);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var companyPartnerModel = CompanyPartnerModel.FromCompanyPartner(addPartnerResult.Data!);

        return SuccessResult(companyPartnerModel);
    }

    // Private Methods

    private async Task<bool> PartnerNotFound(Guid partnerId)
    {
        return !await partnerRepository.AnyPartnerByIdAsync(partnerId);
    }
}
