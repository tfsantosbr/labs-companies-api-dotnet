using Companies.Application.Abstractions.Validations;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Events;
using FluentValidation;

namespace Companies.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add Validators

        services.AddValidatorsFromAssemblyContaining<CreateCompanyCommandValidator>();
        services.AddTransient(typeof(ICommandValidator<>), typeof(CommandValidator<>));

        // Add Command/Query Handlers

        services.AddApplicationHandlersFromAssemblyContaining<CreateCompanyCommandHandler>();

        // Add Event Handlers

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CompanyCreatedDomainEvent>());

        return services;
    }
}
