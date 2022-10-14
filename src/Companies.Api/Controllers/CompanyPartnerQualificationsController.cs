using Companies.Domain.Features.CompanyPartnerQualifications;
using Companies.Infrastructure.Contexts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Controllers;

[ApiController]
[Route("company-partner-qualifications")]
public class CompanyPartnerQualificationsController : ControllerBase
{
    private DbSet<CompanyPartnerQualification> _companyPartnerQualifications;
    public CompanyPartnerQualificationsController(CompaniesContext context)
    {
        _companyPartnerQualifications = context.CompanyPartnerQualifications;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var items = await _companyPartnerQualifications.ToListAsync();

        return Ok(items);
    }
}
