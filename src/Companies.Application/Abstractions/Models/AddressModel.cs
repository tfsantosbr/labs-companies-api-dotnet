using Companies.Application.Abstractions.ValueObjects;

namespace Companies.Application.Abstractions.Models;

public record AddressModel(string PostalCode, string Street, string Number,
    string? Complement, string Neighborhood, string City, string State, string Country)
{
    public static AddressModel FromAddress(Address address) => new(
        address.PostalCode,
        address.Street,
        address.Number,
        address.Complement,
        address.Neighborhood,
        address.City,
        address.State,
        address.Country
    );
}