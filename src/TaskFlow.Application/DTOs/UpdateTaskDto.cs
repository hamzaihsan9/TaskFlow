namespace TaskFlow.Application.DTOs;

/// <summary>
/// DTO for updating an existing task item. All fields are provided
/// (full replacement semantics).
/// </summary>
public class UpdateTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "Pending";
    public string Priority { get; set; } = "Medium";
    public DateTime? DueDate { get; set; }
    public int? CategoryId { get; set; }
}
