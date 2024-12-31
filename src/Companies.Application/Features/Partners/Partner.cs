using Companies.Application.Abstractions.ValueObjects;
using Companies.Application.Features.Companies;

namespace Companies.Application.Features.Partners;

public class Partner
{
    private readonly List<CompanyPartner> _companies = [];

    private Partner()
    {
    }

    public Guid Id { get; private set; }
    public CompleteName CompleteName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public IReadOnlyCollection<CompanyPartner> Companies => _companies.AsReadOnly();

    public static Partner Create(CompleteName completeName, Email email, Guid? id = null)
    {
        var partner = new Partner
        {
            Id = id ?? Guid.NewGuid(),
            CompleteName = completeName,
            Email = email
        };

        return partner;
    }
}
