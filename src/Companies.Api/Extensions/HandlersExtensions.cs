using Companies.Application.Abstractions.Handlers;

namespace Companies.Api.Extensions;

public static class HandlersExtensions
{
    public static IServiceCollection AddHandlersFromAssemblyContaining<T>(this IServiceCollection services)
        where T : class
    {
        var assembly = typeof(T).Assembly;
        IEnumerable<Type> types = assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface);

        foreach (Type type in types)
        {
            Type[] typeInterfaces = type.GetInterfaces();

            foreach (Type typeInterface in typeInterfaces)
            {
                if (typeInterface.IsGenericType &&
                    (typeInterface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                     typeInterface.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                     typeInterface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                    )
                {
                    services.AddTransient(typeInterface, type);
                }
            }
        }

        return services;
    }
}