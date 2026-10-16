namespace TaskFlow.Application.DTOs;

/// <summary>
/// Data Transfer Object for returning category data to API consumers.
/// </summary>
public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TaskCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
