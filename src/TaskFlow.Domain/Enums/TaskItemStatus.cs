namespace TaskFlow.Domain.Enums;

/// <summary>
/// Represents the lifecycle status of a task item.
/// Tasks progress through these states as work is completed.
/// </summary>
public enum TaskItemStatus
{
    /// <summary>Task has been created but work has not started.</summary>
    Pending = 0,

    /// <summary>Task is currently being worked on.</summary>
    InProgress = 1,

    /// <summary>Task has been finished successfully.</summary>
    Completed = 2,

    /// <summary>Task was cancelled and will not be completed.</summary>
    Cancelled = 3
}
