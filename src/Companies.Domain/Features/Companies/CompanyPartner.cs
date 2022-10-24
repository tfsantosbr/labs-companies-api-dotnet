using Companies.Domain.Features.CompanyPartnerQualifications;
using Companies.Domain.Features.Partners;

namespace Companies.Domain.Features.Companies;

public class CompanyPartner
{
    public CompanyPartner(Guid partnerId, int qualificationId, DateOnly entryData)
    {
        PartnerId = partnerId;
        QualificationId = qualificationId;
        EntryData = entryData;
    }

    public Guid CompanyId { get; private set; }
    public Guid PartnerId { get; private set; }
    public int QualificationId { get; private set; }
    public DateOnly EntryData { get; private set; }
    public Company Company { get; private set; } = default!;
    public Partner Partner { get; private set; } = default!;
    public CompanyPartnerQualification Qualification { get; private set; } = default!;

    public void Update(Guid partnerId, int qualificationId, DateOnly entryData)
    {
        PartnerId = partnerId;
        QualificationId = qualificationId;
        EntryData = entryData;
    }
}
