using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Companies.Api.Services.Interfaces;

using Microsoft.IdentityModel.Tokens;

namespace Companies.Api.Services;

public class TokenService : ITokenService
{
    private readonly string _tokenSecret;

    public TokenService(IConfiguration configuration)
    {
        _tokenSecret = configuration["JwtSettings:Secret"]!;
    }

    public string GenerateToken(string userId, string userName, string userEmail)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSecret);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.Email, userEmail),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }

}
