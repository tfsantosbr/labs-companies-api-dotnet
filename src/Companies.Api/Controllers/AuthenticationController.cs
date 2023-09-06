using Companies.Api.Models.Authentication;
using Companies.Api.Services.Interfaces;
using Companies.Domain.Base.Models;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthenticationController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("token")]
    public IActionResult RequestToken(RequestToken request)
    {
        if (request.Username != "test" || request.Password != "test")
            return NotFound(new Notification("User", "User not found"));

        // fake test user
        var userId = new Guid("e111a2b9-8c36-4953-a05a-c2b0aee01809");
        var userName = "test User";
        var userEmail = "test@email.com";

        var token = _tokenService.GenerateToken(userId.ToString(), userName, userEmail);

        return Ok(new { token });
    }
}
