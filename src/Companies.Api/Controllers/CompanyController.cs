using AutoMapper;

using Companies.Domain.Base.Models;
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

    public CompanyController(ICompanyRepository companyRepository, IMediator mediator, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
        _mapper = mapper;
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
        var company = await _companyRepository.GetById(companyId);

        if (company == null)
            return NotFound(new Notification("Company", "Company not found"));

        var companyDetails = _mapper.Map<CompanyDetails>(company);

        return Ok(companyDetails);
    }
}
