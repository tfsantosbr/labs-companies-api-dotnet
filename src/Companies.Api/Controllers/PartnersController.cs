using Companies.Domain.Features.Partners;
using Companies.Domain.Features.Partners.Models;
using Companies.Infrastructure.Contexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Controllers;

[ApiController]
[Route("partners")]
public class PartnersController : ControllerBase
{
    private DbSet<Partner> _partners;

    public PartnersController(CompaniesContext context)
    {
        _partners = context.Partners;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await _partners
            .AsNoTracking()
            .Select(u => new PartnerItem
            {
                Id = u.Id,
                Name = u.CompleteName.ToString(),
                Email = u.Email.ToString()
            })
            .ToListAsync();

        return Ok(items);
    }
}
