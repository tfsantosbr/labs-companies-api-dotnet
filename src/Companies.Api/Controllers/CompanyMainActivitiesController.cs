using Companies.Domain.Features.CompanyMainActivities;
using Companies.Infrastructure.Contexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Controllers;

[ApiController]
[Route("company-main-activities")]
public class CompanyMainActivitiesController : ControllerBase
{
    private DbSet<CompanyMainActivity> _companyMainActivities;
    public CompanyMainActivitiesController(CompaniesContext context)
    {
        _companyMainActivities = context.CompanyMainActivities;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await _companyMainActivities.ToListAsync();

        return Ok(items);
    }
}
