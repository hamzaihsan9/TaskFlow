namespace TaskFlow.Application.DTOs;

/// <summary>
/// DTO for creating a new task item. Used as input for the CreateTask command.
/// </summary>
public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime? DueDate { get; set; }
    public int? CategoryId { get; set; }
}
