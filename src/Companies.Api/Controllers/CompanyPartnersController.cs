using AutoMapper;

using Companies.Domain.Base.Models;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Models;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("companies/{companyId}/partners")]
public class CompanyPartnersController : ControllerBase
{
    private ICompanyRepository _companyRepository;
    private IMediator _mediator;
    private IMapper _mapper;

    public CompanyPartnersController(ICompanyRepository companyRepository, IMediator mediator, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> FindCompanies(Guid companyId)
    {
        if (await _companyRepository.AnyById(companyId))
            return NotFound(new Notification("Company", "Company not found"));

        var partners = await _companyRepository.GetPartners(companyId);

        return Ok(partners);
    }

    [HttpPost]
    public async Task<IActionResult> AddPartner(Guid companyId, [FromBody] AddPartner request)
    {
        var response = await _mediator.Send(request);

        if (response.HasNotifications)
            return NotFound(response.Notifications);

        var addedPartnerDetails = _mapper.Map<CompanyPartnerModel>(response.Data);

        return Created($"companies/{companyId}/partners/{addedPartnerDetails.PartnerId}", addedPartnerDetails);
    }

    [HttpDelete("{partnerId}")]
    public async Task<IActionResult> RemoveCompany(Guid companyId, Guid partnerId)
    {
        var response = await _mediator.Send(new RemovePartner(companyId, partnerId));

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        return NoContent();
    }
}
