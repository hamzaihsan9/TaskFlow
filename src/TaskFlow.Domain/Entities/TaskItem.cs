using TaskFlow.Domain.Common;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities;

/// <summary>
/// Represents a task item in the system — the core business entity.
/// A task has a title, description, status, priority, optional due date,
/// and belongs to an optional category.
/// </summary>
public class TaskItem : BaseEntity
{
    /// <summary>
    /// The title/name of the task.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of what the task involves.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Current lifecycle status of the task.
    /// </summary>
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;

    /// <summary>
    /// Priority level indicating how urgently the task should be addressed.
    /// </summary>
    public Priority Priority { get; set; } = Priority.Medium;

    /// <summary>
    /// Optional deadline for the task.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Foreign key to the category this task belongs to. Null if uncategorized.
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Navigation property to the parent category.
    /// </summary>
    public Category? Category { get; set; }
}
