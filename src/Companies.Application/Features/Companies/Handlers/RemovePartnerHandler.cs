using Companies.Application.Base.Handlers;
using Companies.Application.Base.Models;
using Companies.Application.Base.Persistence;
using Companies.Application.Features.Companies.Commands;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Handlers;

public class RemovePartnerFromCompanyHandler : CommandHandler, IHandler<RemovePartnerFromCompany, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public RemovePartnerFromCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(RemovePartnerFromCompany request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetById(request.CompanyId);

        if (company == null)
            return ErrorResponse("CompanyPartner", "Company not found");

        var partner = company.GetPartner(request.PartnerId);

        if (partner == null)
            return ErrorResponse("CompanyPartner", "Partner not found");

        company.RemovePartner(partner);

        await _unitOfWork.CommitAsync();

        return Response.Ok();
    }
}
