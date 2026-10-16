namespace TaskFlow.Domain.Enums;

/// <summary>
/// Represents the priority level of a task item.
/// Higher priority tasks should be addressed first.
/// </summary>
public enum Priority
{
    /// <summary>Low priority — can be addressed when convenient.</summary>
    Low = 0,

    /// <summary>Medium priority — standard importance.</summary>
    Medium = 1,

    /// <summary>High priority — should be addressed soon.</summary>
    High = 2,

    /// <summary>Critical priority — requires immediate attention.</summary>
    Critical = 3
}
