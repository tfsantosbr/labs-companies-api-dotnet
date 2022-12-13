using Companies.Domain.Features.Companies.Repositories;
using Companies.Domain.Features.Partners.Repositories;
using Companies.Infrastructure.Repositories;

namespace Companies.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<IPartnerRepository, PartnerRepository>();
        
        return services;
    }
}
