using Companies.Domain.Base.ValueObjects;

namespace Companies.Domain.Features.Partners;

public class Partner
{
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
}
