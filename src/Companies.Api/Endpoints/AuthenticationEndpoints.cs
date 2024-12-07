using Companies.Api.Extensions.Endpoints;
using Companies.Api.Models.Authentication;
using Companies.Api.Services.Interfaces;
using Companies.Application.Abstractions.Results;

namespace Companies.Api.Endpoints;

public class AuthenticationEndpoints : IEndpointBuilder
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("authentication").WithTags("Authentication");

        group.MapPost("/token", RequestToken)
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    public static IResult RequestToken(
        CreateTokenRequest request,
        ITokenService tokenService)
    {
        if (request.Username != "test" || request.Password != "test")
        {
            return Results.NotFound(new Error("User", "User not found"));
        }

        // fake test user
        var userId = new Guid("e111a2b9-8c36-4953-a05a-c2b0aee01809");
        var userName = "test User";
        var userEmail = "test@email.com";

        var token = tokenService.GenerateToken(userId.ToString(), userName, userEmail);

        return Results.Ok(new { token });
    }
}
