using Companies.Domain.Base.Models;
using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

namespace Companies.Domain.Features.Companies.Handlers;

public class RemoveCompanyHandler : IRequestHandler<RemoveCompany, Response>
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

    // Private Methods

    private Response ErrorResponse(string code, string errorMessage)
    {
        return Response.Error(new Notification(code, errorMessage));
    }
}
