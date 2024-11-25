namespace Companies.Application.Features.Partners.Models;

public record PartnerItem(Guid Id, string FirstName, string LastName, string Email)
{
    public static PartnerItem FromPartner(Partner partner)
    {
        return new PartnerItem(
            Id: partner.Id,
            FirstName: partner.CompleteName.FirstName,
            LastName: partner.CompleteName.LastName,
            Email: partner.Email.Address
        );
    }
}
