using AutoMapper;

using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Commands.ImportCompanies;
using Companies.Application.Features.Companies.Commands.RemoveCompany;
using Companies.Application.Features.Companies.Commands.UpdateCompany;
using Companies.Application.Features.Companies.Models;
using Companies.Application.Features.Companies.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("companies")]
[Authorize]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> _logger;
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly IHandler<CreateCompanyCommand, Response<Company>> _createCompanyHandler;
    private readonly IHandler<ImportCompaniesCommand, Response> _importCompaniesHandler;
    private readonly IHandler<UpdateCompanyCommand, Response> _updateCompanyHandler;
    private readonly IHandler<RemoveCompanyCommand, Response> _removeCompanyHandler;

    public CompanyController(
        IMapper mapper,
        ICompanyRepository companyRepository,
        IHandler<CreateCompanyCommand, Response<Company>> createCompanyHandler,
        IHandler<UpdateCompanyCommand, Response> updateCompanyHandler,
        IHandler<RemoveCompanyCommand, Response> removeCompanyHandler,
        ILogger<CompanyController> logger,
        IHandler<ImportCompaniesCommand, Response> importCompaniesHandler)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _createCompanyHandler = createCompanyHandler;
        _updateCompanyHandler = updateCompanyHandler;
        _removeCompanyHandler = removeCompanyHandler;
        _logger = logger;
        _importCompaniesHandler = importCompaniesHandler;
    }

    [HttpGet]
    public async Task<IActionResult> FindCompanies([FromQuery] CompanyParameters parameters)
    {
        var pagedItems = await _companyRepository.Find(parameters);

        return Ok(pagedItems);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand request)
    {
        var response = await _createCompanyHandler.Handle(request);

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        var createdCompanyDetails = _mapper.Map<CompanyDetails>(response.Data);

        return Created($"companies/{createdCompanyDetails.Id}", createdCompanyDetails);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportCompanies([FromBody] ImportCompaniesCommand request)
    {
        var response = await _importCompaniesHandler.Handle(request);

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        return Accepted();
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetCompany(Guid companyId)
    {
        var company = await _companyRepository.GetById(companyId);

        if (company == null)
            return NotFound(new Notification("Company", "Company not found"));

        var companyDetails = _mapper.Map<CompanyDetails>(company);

        _logger.LogInformation("Requested company with Id: {@CompanyId} and Name: {@CompanyName}", companyId, company.Name);

        return Ok(companyDetails);
    }

    [HttpPut("{companyId}")]
    public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] UpdateCompanyCommand request)
    {
        request.CompanyId = companyId;

        var response = await _updateCompanyHandler.Handle(request);

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        return NoContent();
    }

    [HttpDelete("{companyId}")]
    public async Task<IActionResult> RemoveCompany(Guid companyId)
    {
        var response = await _removeCompanyHandler.Handle(new RemoveCompanyCommand(companyId));

        if (response.HasNotifications)
            return BadRequest(response.Notifications);

        return NoContent();
    }
}
