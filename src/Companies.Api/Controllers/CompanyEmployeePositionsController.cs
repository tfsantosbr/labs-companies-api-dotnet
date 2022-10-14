using Companies.Domain.Features.CompanyEmployeePositions;
using Companies.Infrastructure.Contexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Controllers;

[ApiController]
[Route("company-employee-sositions")]
public class CompanyEmployeePositionsController : ControllerBase
{
    private DbSet<CompanyEmployeePosition> _companyEmployeePositions;
    public CompanyEmployeePositionsController(CompaniesContext context)
    {
        _companyEmployeePositions = context.CompanyEmployeePositions;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await _companyEmployeePositions.ToListAsync();

        return Ok(items);
    }
}
