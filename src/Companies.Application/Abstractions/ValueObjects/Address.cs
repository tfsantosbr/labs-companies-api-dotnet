namespace Companies.Application.Abstractions.ValueObjects;

public record Address
{
    public Address(string postalCode, string street, string number, string? complement, string neighborhood,
        string city, string state, string country)
    {
        PostalCode = postalCode;
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
    }

    private Address()
    {
    }

    public string PostalCode { get; private set; } = default!;
    public string Street { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public string? Complement { get; private set; }
    public string Neighborhood { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string Country { get; private set; } = default!;

    public override string ToString()
    {
        var complement = Complement == null ? string.Empty : $", {Complement}";

        return $"{Street}, {Number}{complement}, {Neighborhood}, {City}, {State}, {Country}, {PostalCode}";
    }
}

