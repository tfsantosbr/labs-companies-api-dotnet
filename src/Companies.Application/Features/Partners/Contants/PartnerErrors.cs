using Companies.Application.Abstractions.Results;

namespace Companies.Application.Features.Partners.Contants;

public static class PartnerErrors
{
    public static Error EmailAlreadyExists(string email)
        => new(nameof(EmailAlreadyExists), $"The e-mail {email} is already in use.");

    public static Error PartnerNotFound(Guid partnerId)
        => new(nameof(PartnerNotFound), $"Partner with id '{partnerId}' not found.");
}
