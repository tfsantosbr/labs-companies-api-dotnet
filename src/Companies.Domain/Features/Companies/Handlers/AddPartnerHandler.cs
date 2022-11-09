using Companies.Domain.Base.Handlers;
using Companies.Domain.Base.Models;
using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Commands.Validators;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

namespace Companies.Domain.Features.Companies.Handlers;

public class AddPartnerHandler : CommandHandler, IRequestHandler<AddPartner, Response>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public AddPartnerHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    // Implementations

    public async Task<Response> Handle(AddPartner request, CancellationToken cancellationToken)
    {
        if (IsInvalidRequest(request, out var notifications))
            return RequestErrorsResponse(notifications);

        var company = await _companyRepository.GetById(request.CompanyId);

        if (company == null)
            return ErrorResponse("Company", "Company not found");

        if (await PartnerNotFound(request.PartnerId))
            return ErrorResponse("CompanyPartner", "Partner not found");

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

    private bool IsInvalidRequest(AddPartner request, out IEnumerable<Notification> notifications)
    {
        var validator = new AddPartnerValidator();
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

    private async Task<bool> PartnerNotFound(Guid partnerId)
    {
        return !await _companyRepository.AnyPartnerById(partnerId);
    }
}
