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

    public CompanyController(ICompanyRepository companyRepository, IMediator mediator)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindCompanies([FromQuery] CompanyParameters parameters)
    {
        var pagedItems = await _companyRepository.Find(parameters);

        return Ok(pagedItems);
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetCompany(Guid companyId)
    {
        var company = await _companyRepository.GetCompanyWithPartnersById(companyId);

        // TODO

        return Ok(pagedItems);
    }
}
