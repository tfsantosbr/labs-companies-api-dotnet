using Companies.Application.Abstractions.ValueObjects;

namespace Companies.Application.Abstractions.Models;

public record PhoneModel(string CountryCode, string Number)
{
    public static PhoneModel FromPhone(Phone phone) => new(phone.CountryCode, phone.Number);
}
