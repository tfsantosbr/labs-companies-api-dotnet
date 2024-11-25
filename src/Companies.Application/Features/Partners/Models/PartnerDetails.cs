namespace Companies.Application.Features.Partners.Models;

public record PartnerDetails(Guid Id, string FirstName, string LastName, string Email)
{
    public static PartnerDetails FromPartner(Partner partner)
    {
        return new PartnerDetails(
            Id: partner.Id,
            FirstName: partner.CompleteName.FirstName,
            LastName: partner.CompleteName.LastName,
            Email: partner.Email.Address
        );
    }
}
