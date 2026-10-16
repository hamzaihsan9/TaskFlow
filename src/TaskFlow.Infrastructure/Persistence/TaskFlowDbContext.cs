using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core DbContext for the TaskFlow application.
/// This is the bridge between your domain entities and the database.
/// It manages database connections, tracks changes, and maps entities to tables.
/// </summary>
public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// DbSet for querying and saving TaskItem entities.
    /// </summary>
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    /// <summary>
    /// DbSet for querying and saving Category entities.
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>
    /// Configures entity mappings and relationships using the Fluent API.
    /// This is called once when the model is being created.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the current assembly
        // (finds all IEntityTypeConfiguration<T> implementations)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskFlowDbContext).Assembly);
    }

    /// <summary>
    /// Automatically sets audit timestamps before saving changes.
    /// This is an example of a cross-cutting concern handled at the infrastructure level.
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Domain.Common.BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
