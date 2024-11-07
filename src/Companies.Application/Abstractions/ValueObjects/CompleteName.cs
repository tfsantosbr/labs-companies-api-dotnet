namespace Companies.Application.Abstractions.ValueObjects;

public record CompleteName
{
    public CompleteName(string firstName, string lastname)
    {
        FirstName = firstName;
        LastName = lastname;
    }

    private CompleteName()
    {
    }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
