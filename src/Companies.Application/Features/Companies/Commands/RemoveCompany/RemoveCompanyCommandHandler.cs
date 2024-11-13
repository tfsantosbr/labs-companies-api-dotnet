using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Commands.RemoveCompany;

public class RemoveCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork) 
    : CommandHandler, ICommandHandler<RemoveCompanyCommand>
{
    public async Task<Result> Handle(RemoveCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);

        if (company == null)
            return ErrorResult(CompanyErrors.CompanyNotFound(request.CompanyId));

        companyRepository.Remove(company);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
