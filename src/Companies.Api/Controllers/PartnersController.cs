using Companies.Domain.Base.ValueObjects;
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
    private CompaniesContext _context;

    public PartnersController(CompaniesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await _context.Partners
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

    [HttpPost]
    public async Task<IActionResult> Create(CreatePartner request)
    {
        if (await IsDuplicatedEmail(request.Email))
            return BadRequest(new
            {
                Code = "Partner",
                Message = $"Partner e-mail {request.Email} is already in use"
            });

        var partner = new Partner(
            new CompleteName(request.FirstName, request.LastName),
            new Email(request.Email)
            );

        await _context.AddAsync(partner);
        await _context.SaveChangesAsync();

        var createdPartner = new PartnerItem
        {
            Id = partner.Id,
            Name = partner.CompleteName.ToString(),
            Email = partner.Email.ToString()
        };

        return Created($"partners/{createdPartner.Id}", createdPartner);
    }

    [HttpDelete("{partnerId}")]
    public async Task<IActionResult> Delete(Guid partnerId)
    {
        var partner = await _context.Partners
            .FirstOrDefaultAsync(p => p.Id == partnerId);

        if (partner == null)
            return NotFound(new { Code = "Partner", Message = "Partner not found" });

        _context.Partners.Remove(partner);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Private Methods

    private async Task<bool> IsDuplicatedEmail(string email)
    {
        return await _context.Partners.AsNoTracking().AnyAsync(p => p.Email.Address == email);
    }
}
