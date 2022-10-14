using Companies.Domain.Base.ValueObjects;

namespace Companies.Domain.Features.Users;

public class User
{
    public User(CompleteName completeName, Email email, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CompleteName = completeName;
        Email = email;
    }

    private User()
    {
    }

    public Guid Id { get; private set; }
    public CompleteName CompleteName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
}
