using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence;
using TaskFlow.Infrastructure.Repositories;

namespace TaskFlow.Infrastructure;

/// <summary>
/// Extension method to register all Infrastructure layer services.
/// Configures EF Core, database connection, and repository bindings.
/// </summary>
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register EF Core with SQLite
        services.AddDbContext<TaskFlowDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("TaskFlowDb"),
                b => b.MigrationsAssembly(typeof(TaskFlowDbContext).Assembly.FullName)));

        // Register repositories
        // The DI container will inject the correct implementation
        // whenever code asks for the interface
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
