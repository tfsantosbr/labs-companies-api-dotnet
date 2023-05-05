using Companies.Domain.Base.Handlers;
using Companies.Domain.Base.Models;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Handlers;
using Companies.Domain.Features.Companies.Repositories;
using Companies.Domain.Features.Partners.Repositories;
using Companies.Infrastructure.Repositories;

namespace Companies.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Companies
        
        services.AddTransient<IHandler<CreateCompany, Response<Company>>, CreateCompanyHandler>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        
        // Partners

        services.AddTransient<IPartnerRepository, PartnerRepository>();
        
        return services;
    }
}
