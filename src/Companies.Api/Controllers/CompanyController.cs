using AutoMapper;

using Companies.Domain.Base.Handlers;
using Companies.Domain.Base.Models;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Models;
using Companies.Domain.Features.Companies.Repositories;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("companies")]
public class CompanyController : ControllerBase
{
    private ICompanyRepository _companyRepository;
    private IMediator _mediator;
    private IMapper _mapper;

    private IHandler<CreateCompany, Response<Company>> _createCompanyHandler;

    public CompanyController(
        ICompanyRepository companyRepository, 
        IMediator mediator, 
        IMapper mapper, 
        IHandler<CreateCompany, Response<Company>> createCompanyHandler)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
        _mapper = mapper;
        _createCompanyHandler = createCompanyHandler;
    }

    [HttpGet]
    public async Task<IActionResult> FindCompanies([FromQuery] CompanyParameters parameters)
    {
        var pagedItems = await _companyRepository.Find(parameters);

        return Ok(pagedItems);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompany request)
    {
        var response = await _createCompanyHandler.Handle(request);

        if (response.HasNotifications)
            return NotFound(response.Notifications);

        var createdCompanyDetails = _mapper.Map<CompanyDetails>(response.Data);

        return Created($"companies/{createdCompanyDetails.Id}", createdCompanyDetails);
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetCompany(Guid companyId)
    {
        var company = await _companyRepository.GetById(companyId);

        if (company == null)
            return NotFound(new Notification("Company", "Company not found"));

        var companyDetails = _mapper.Map<CompanyDetails>(company);

        return Ok(companyDetails);
    }

    [HttpPut("{companyId}")]
    public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] UpdateCompany request)
    {
        request.CompanyId = companyId;

        var response = await _mediator.Send(request);

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        return NoContent();
    }

    [HttpDelete("{companyId}")]
    public async Task<IActionResult> RemoveCompany(Guid companyId)
    {
        var response = await _mediator.Send(new RemoveCompany(companyId));

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        return NoContent();
    }
}
