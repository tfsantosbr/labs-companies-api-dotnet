using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Commands.RemovePartnerFromCompany;

public class RemovePartnerFromCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        : AbstractHandler, ICommandHandler<RemovePartnerFromCompanyCommand>
{
    public async Task<Result> HandleAsync(RemovePartnerFromCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);

        if (company == null)
            return ErrorResult(CompanyErrors.CompanyNotFound(request.CompanyId));

        var removePartnerResult = company.RemovePartner(request.PartnerId);

        if (removePartnerResult.IsFailure)
            return ErrorResult(removePartnerResult.Notifications);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
