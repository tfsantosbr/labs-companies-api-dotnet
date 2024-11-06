using Companies.Application.Base.ValueObjects;
using Companies.Application.Features.Companies;

namespace Companies.Application.Features.Partners;

public class Partner
{
    private readonly List<CompanyPartner> _companies = new List<CompanyPartner>();

    public Partner(CompleteName completeName, Email email, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CompleteName = completeName;
        Email = email;
    }

    private Partner()
    {
    }

    public Guid Id { get; private set; }
    public CompleteName CompleteName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public IReadOnlyCollection<CompanyPartner> Companies => _companies.AsReadOnly();
}
