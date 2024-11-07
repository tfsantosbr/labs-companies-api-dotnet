using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Models;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Commands.RemoveCompany;

public class RemoveCompanyCommandHandler : CommandHandler, IHandler<RemoveCompanyCommand, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public RemoveCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(RemoveCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetById(request.CompanyId);

        if (company == null)
            return ErrorResponse("Company", "Company not found");

        _companyRepository.Remove(company);

        await _unitOfWork.CommitAsync();

        return Response.Ok();
    }
}
