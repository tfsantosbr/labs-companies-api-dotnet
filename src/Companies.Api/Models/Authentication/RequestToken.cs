namespace Companies.Api.Models.Authentication;

public class RequestToken
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
