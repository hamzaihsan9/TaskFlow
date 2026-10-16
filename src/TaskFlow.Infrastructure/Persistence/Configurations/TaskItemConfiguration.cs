using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for the TaskItem entity.
/// Uses the Fluent API to define table structure, constraints, and relationships.
/// This approach keeps entity classes clean (no data annotations needed).
/// </summary>
public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        // Table name
        builder.ToTable("Tasks");

        // Primary key
        builder.HasKey(t => t.Id);

        // Property configurations
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>()  // Store enum as string in DB for readability
            .HasMaxLength(50);

        builder.Property(t => t.Priority)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        // Relationship: Task belongs to an optional Category
        builder.HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);  // If category deleted, set FK to null
    }
}
