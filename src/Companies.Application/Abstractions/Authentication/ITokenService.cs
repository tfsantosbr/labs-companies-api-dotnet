namespace Companies.Application.Abstractions.Authentication;

public interface ITokenService
{
    string GenerateToken(string userId, string userName, string userEmail);
}
