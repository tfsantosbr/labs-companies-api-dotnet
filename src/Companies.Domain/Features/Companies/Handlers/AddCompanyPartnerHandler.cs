using Companies.Domain.Base.Handlers;
using Companies.Domain.Base.Models;
using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Commands.Validators;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

namespace Companies.Domain.Features.Companies.Handlers;

public class AddCompanyPartnerHandler : CommandHandler, IRequestHandler<AddCompanyPartner, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public AddCompanyPartnerHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(AddCompanyPartner request, CancellationToken cancellationToken)
    {
        if (IsInvalidRequest(request, out var notifications))
            return RequestErrorsResponse(notifications);

        var company = await _companyRepository.GetCompanyWithPartnersById(request.CompanyId);

        if (company == null)
            return ErrorResponse("CompanyPartner", "Company not found");

        if (IsPartnerAlreadyLinkedInCompany(company.Partners, request.PartnerId))
            return ErrorResponse("CompanyPartner", "This partner is already linked with this company");

        var partner = new CompanyPartner(
            partnerId: request.PartnerId,
            qualificationId: request.QualificationId,
            joinedAt: request.JoinedAt
        );

        company.AddPartner(partner);

        await _unitOfWork.CommitAsync();

        return Response.Ok();
    }

    // Private Methods

    private bool IsInvalidRequest(AddCompanyPartner request, out IEnumerable<Notification> notifications)
    {
        var validator = new AddCompanyPartnerValidator();
        var result = validator.Validate(request);

        notifications = result.Errors.Select(e =>
            new Notification(e.PropertyName, e.ErrorMessage)
        );

        return !result.IsValid;
    }
    
    private bool IsPartnerAlreadyLinkedInCompany(IReadOnlyCollection<CompanyPartner> partners, Guid partnerId)
    {
        return partners.Any(p => p.PartnerId == partnerId);
    }
}
