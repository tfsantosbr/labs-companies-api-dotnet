using Companies.Domain.Base.Handlers;
using Companies.Domain.Base.Models;
using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Commands.Validators;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

namespace Companies.Domain.Features.Companies.Handlers;

public class RemovePartnerHandler : CommandHandler, IRequestHandler<RemovePartner, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public RemovePartnerHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(RemovePartner request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyWithPartnersById(request.CompanyId);

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
