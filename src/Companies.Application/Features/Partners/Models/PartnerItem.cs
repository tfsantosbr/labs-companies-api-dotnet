namespace Companies.Application.Features.Partners.Models;

public class PartnerItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}
