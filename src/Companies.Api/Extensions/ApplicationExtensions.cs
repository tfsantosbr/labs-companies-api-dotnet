using Companies.Application.Abstractions.Validations;
using Companies.Application.Features.Companies.Commands.CreateCompany;

namespace Companies.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add Validators

        services.AddTransient(typeof(ICommandValidator<>), typeof(CommandValidator<>));

        // Add Handlers

        services.AddHandlersFromAssemblyContaining<CreateCompanyCommandHandler>();

        return services;
    }
}
