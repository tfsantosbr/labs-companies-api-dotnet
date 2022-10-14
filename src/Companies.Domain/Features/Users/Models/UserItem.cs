namespace Companies.Domain.Features.Users.Models;

public class UserItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}
