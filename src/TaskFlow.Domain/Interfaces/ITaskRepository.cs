using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Interfaces;

/// <summary>
/// Repository interface specific to TaskItem entities.
/// Extends the generic repository with task-specific query methods.
/// </summary>
public interface ITaskRepository : IGenericRepository<TaskItem>
{
    /// <summary>
    /// Retrieves all tasks filtered by their status.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    /// <returns>Tasks matching the specified status.</returns>
    Task<IReadOnlyList<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status);

    /// <summary>
    /// Retrieves all tasks belonging to a specific category.
    /// </summary>
    /// <param name="categoryId">The category ID to filter by.</param>
    /// <returns>Tasks in the specified category.</returns>
    Task<IReadOnlyList<TaskItem>> GetTasksByCategoryAsync(int categoryId);

    /// <summary>
    /// Retrieves a task by ID including its related category data.
    /// </summary>
    /// <param name="id">The task ID.</param>
    /// <returns>The task with category loaded, or null.</returns>
    Task<TaskItem?> GetTaskWithCategoryAsync(int id);
}
