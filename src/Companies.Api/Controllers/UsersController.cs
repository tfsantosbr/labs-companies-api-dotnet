using Companies.Domain.Features.Users;
using Companies.Domain.Features.Users.Models;
using Companies.Infrastructure.Contexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private DbSet<User> _users;

    public UsersController(CompaniesContext context)
    {
        _users = context.Users;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await _users
            .AsNoTracking()
            .Select(u => new UserItem
            {
                Id = u.Id,
                Name = u.CompleteName.ToString(),
                Email = u.Email.ToString()
            })
            .ToListAsync();

        return Ok(items);
    }
}
