using Companies.Application.Base.ValueObjects;
using Companies.Application.Features.Partners;
using Companies.Application.Features.Partners.Models;
using Companies.Application.Features.Partners.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("partners")]
public class PartnersController : ControllerBase
{
    private readonly IPartnerRepository _partnerRepository;

    public PartnersController(IPartnerRepository partnerRepository)
    {
        _partnerRepository = partnerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await _partnerRepository.List();

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

        await _partnerRepository.Add(partner);

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
        var partner = await _partnerRepository.GetById(partnerId);

        if (partner == null)
            return NotFound(new { Code = "PARTNER_NOT_FOUND", Message = "Partner not found" });

        await _partnerRepository.Remove(partner);

        return NoContent();
    }

    // Private Methods

    private async Task<bool> IsDuplicatedEmail(string email)
    {
        return await _partnerRepository.IsDuplicatedEmail(email);
    }
}
