namespace Companies.Application.Base.ValueObjects;

public class Email
{
    public Email(string address)
    {
        Address = address;
    }

    private Email()
    {
    }

    public string Address { get; private set; } = default!;

    public override string ToString()
    {
        return $"{Address}";
    }
}

