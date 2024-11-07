namespace Companies.Application.Abstractions.ValueObjects;

public record Phone
{
    public Phone(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }

    public Phone()
    {
    }

    public string CountryCode { get; private set; } = default!;
    public string Number { get; private set; } = default!;

    public override string ToString()
    {
        return $"{CountryCode} {Number}";
    }
}
