using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Models;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Features.Companies.Repositories;

namespace Companies.Application.Features.Companies.Commands.RemovePartnerFromCompany;

public class RemovePartnerFromCompanyCommandHandler : CommandHandler, IHandler<RemovePartnerFromCompanyCommand, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public RemovePartnerFromCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(RemovePartnerFromCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetById(request.CompanyId);

        if (company == null)
            return ErrorResponse("CompanyPartner", "Company not found");

        var partner = company.Partners.FirstOrDefault(p => p.PartnerId == request.PartnerId);

        if (partner == null)
            return ErrorResponse("CompanyPartner", "Partner not found");

        company.RemovePartner(partner);

        await _unitOfWork.CommitAsync();

        return Response.Ok();
    }
}
