using Companies.Application.Features.CompanyPartnerQualifications;
using Companies.Application.Features.Partners;

namespace Companies.Application.Features.Companies;

public class CompanyPartner
{
    // Constructors

    public CompanyPartner(Guid companyId, Guid partnerId, int qualificationId, DateOnly joinedAt)
    {
        CompanyId = companyId;
        PartnerId = partnerId;
        QualificationId = qualificationId;
        JoinedAt = joinedAt;
    }

    private CompanyPartner()
    {
    }

    // Properties

    public Guid CompanyId { get; private set; }
    public Guid PartnerId { get; private set; }
    public int QualificationId { get; private set; }
    public DateOnly JoinedAt { get; private set; }
    public Company Company { get; private set; } = default!;
    public Partner Partner { get; private set; } = default!;
    public CompanyPartnerQualification Qualification { get; private set; } = default!;

    // Public Methods

    public void Update(Guid partnerId, int qualificationId, DateOnly joinedAt)
    {
        PartnerId = partnerId;
        QualificationId = qualificationId;
        JoinedAt = joinedAt;
    }
}
