using TaskFlow.Domain.Common;

namespace TaskFlow.Domain.Entities;

/// <summary>
/// Represents a category used to organize and group task items.
/// Categories provide a way to classify tasks (e.g., "Work", "Personal", "Urgent").
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// The display name of the category.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description providing more context about the category.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Navigation property — collection of tasks belonging to this category.
    /// </summary>
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
