using Companies.Application.Base.Handlers;
using Companies.Application.Base.Models;
using Companies.Application.Base.Persistence;
using Companies.Application.Features.Companies.Commands;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Handlers;

public class RemoveCompanyHandler : CommandHandler, IHandler<RemoveCompany, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public RemoveCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(RemoveCompany request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetById(request.CompanyId);

        if (company == null)
            return ErrorResponse("Company", "Company not found");

        _companyRepository.Remove(company);

        await _unitOfWork.CommitAsync();

        return Response.Ok();
    }
}
