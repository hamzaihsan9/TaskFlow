using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Application.Behaviors;

namespace TaskFlow.Application;

/// <summary>
/// Extension method to register all Application layer services.
/// This keeps DI configuration clean and organized by layer.
/// </summary>
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register all MediatR handlers from this assembly
        services.AddMediatR(typeof(ServiceRegistration).Assembly);

        // Register all FluentValidation validators from this assembly
        services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);

        // Register the validation pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
