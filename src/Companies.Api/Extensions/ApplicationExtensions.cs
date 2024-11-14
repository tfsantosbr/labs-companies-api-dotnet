using Companies.Application.Abstractions.Validations;
using Companies.Application.Features.Companies.Commands.CreateCompany;

using FluentValidation;

namespace Companies.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add Validators

        services.AddValidatorsFromAssemblyContaining<CreateCompanyCommandValidator>();
        services.AddTransient(typeof(ICommandValidator<>), typeof(CommandValidator<>));

        // Add Handlers

        services.AddApplicationHandlersFromAssemblyContaining<CreateCompanyCommandHandler>();

        return services;
    }
}
