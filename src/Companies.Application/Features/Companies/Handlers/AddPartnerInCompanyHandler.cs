using Companies.Application.Base.Handlers;
using Companies.Application.Base.Models;
using Companies.Application.Base.Persistence;
using Companies.Application.Features.Companies.Commands;
using Companies.Application.Features.Companies.Commands.Validators;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Features.Partners.Repositories;

namespace Companies.Application.Features.Companies.Handlers;

public class AddPartnerInCompanyHandler : CommandHandler<CompanyPartner>, IHandler<AddPartnerInCompany, Response<CompanyPartner>>
{
    // Fields

    private readonly ICompanyRepository _companyRepository;
    private readonly IPartnerRepository _partnerRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Constructors

    public AddPartnerInCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork,
        IPartnerRepository partnerRepository)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
        _partnerRepository = partnerRepository;
    }

    // Implementations

    public async Task<Response<CompanyPartner>> Handle(AddPartnerInCompany request, CancellationToken cancellationToken)
    {
        if (IsInvalidRequest(request, out var notifications))
            return RequestErrorsResponse(notifications);

        var company = await _companyRepository.GetById(request.CompanyId);

        if (company == null)
            return ErrorResponse("CompanyPartner", "Company not found");

        if (await PartnerNotFound(request.PartnerId))
            return ErrorResponse("CompanyPartner", "Partner not found");

        if (IsPartnerAlreadyLinkedInCompany(company.Partners, request.PartnerId))
            return ErrorResponse("CompanyPartner", "This partner is already linked with this company");

        var partner = new CompanyPartner(
            partnerId: request.PartnerId,
            qualificationId: request.QualificationId,
            joinedAt: DateOnly.FromDateTime(request.JoinedAt)
        );

        company.AddPartner(partner);

        await _unitOfWork.CommitAsync();

        return Response<CompanyPartner>.Ok(partner);
    }

    // Private Methods

    private bool IsInvalidRequest(AddPartnerInCompany request, out IEnumerable<Notification> notifications)
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
        return !await _partnerRepository.AnyPartnerById(partnerId);
    }
}
