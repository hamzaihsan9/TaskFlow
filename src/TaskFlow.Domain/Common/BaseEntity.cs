namespace TaskFlow.Domain.Common;

/// <summary>
/// Base class for all domain entities providing common audit properties.
/// Every entity in the system inherits from this to get automatic
/// tracking of creation and modification timestamps.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// UTC timestamp when the entity was first created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// UTC timestamp when the entity was last modified.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
