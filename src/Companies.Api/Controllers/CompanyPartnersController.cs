using AutoMapper;

using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Commands.AddPartnerInCompany;
using Companies.Application.Features.Companies.Commands.RemovePartnerFromCompany;
using Companies.Application.Features.Companies.Models;
using Companies.Application.Features.Companies.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("companies/{companyId}/partners")]
public class CompanyPartnersController : ControllerBase
{
    private ICompanyRepository _companyRepository;
    private IMapper _mapper;
    private readonly IHandler<AddPartnerInCompany, Response<CompanyPartner>> _addPartnerInCompanyHandler;
    private readonly IHandler<RemovePartnerFromCompanyCommand, Response> _removePartnerFromCompanyHandler;

    public CompanyPartnersController(
        ICompanyRepository companyRepository,
        IMapper mapper,
        IHandler<AddPartnerInCompany, Response<CompanyPartner>> addPartnerInCompanyHandler,
        IHandler<RemovePartnerFromCompanyCommand, Response> removePartnerFromCompanyHandler)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _addPartnerInCompanyHandler = addPartnerInCompanyHandler;
        _removePartnerFromCompanyHandler = removePartnerFromCompanyHandler;
    }

    [HttpGet]
    public async Task<IActionResult> FindPartners(Guid companyId)
    {
        if (!await _companyRepository.AnyById(companyId))
            return NotFound(new Notification("Company", "Company not found"));

        var partners = await _companyRepository.GetPartners(companyId);

        return Ok(partners);
    }

    [HttpPost]
    public async Task<IActionResult> AddPartnerInCompany(Guid companyId, [FromBody] AddPartnerInCompany request)
    {
        request.CompanyId = companyId;

        var response = await _addPartnerInCompanyHandler.Handle(request);

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        var addedPartnerDetails = _mapper.Map<CompanyPartnerModel>(response.Data);

        return Created($"companies/{companyId}/partners/{addedPartnerDetails.PartnerId}", addedPartnerDetails);
    }

    [HttpDelete("{partnerId}")]
    public async Task<IActionResult> RemovePartnerFromCompany(Guid companyId, Guid partnerId)
    {
        var response = await _removePartnerFromCompanyHandler.Handle(new RemovePartnerFromCompanyCommand(companyId, partnerId));

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        return NoContent();
    }
}
