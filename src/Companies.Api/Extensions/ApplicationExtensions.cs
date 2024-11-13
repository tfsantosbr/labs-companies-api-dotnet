using Companies.Application.Abstractions.Validations;
using Companies.Application.Features.Companies.Commands.CreateCompany;

using FluentValidation;

namespace Companies.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add Validators

        services.AddValidatorsFromAssemblyContaining<CreateCompanyCommandValidator>();
        services.AddTransient(typeof(ICommandValidator<>), typeof(CommandValidator<>));

        // Add Handlers

        services.AddHandlersFromAssemblyContaining<CreateCompanyCommandHandler>();

        return services;
    }
}
