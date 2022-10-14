using Companies.Domain.Features.CompanyPartnerQualifications;
using Companies.Domain.Features.Users;

namespace Companies.Domain.Features.Companies;

public class CompanyPartner
{
    public CompanyPartner(Guid userId, int qualificationId, DateOnly entryData)
    {
        UserId = userId;
        QualificationId = qualificationId;
        EntryData = entryData;
    }

    public Guid CompanyId { get; private set; }
    public Guid UserId { get; private set; }
    public int QualificationId { get; private set; }
    public DateOnly EntryData { get; private set; }
    public Company Company { get; private set; } = default!;
    public User User { get; private set; } = default!;
    public CompanyPartnerQualification Qualification { get; private set; } = default!;

    public void Update(Guid userId, int qualificationId, DateOnly entryData)
    {
        UserId = userId;
        QualificationId = qualificationId;
        EntryData = entryData;
    }
}
