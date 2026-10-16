using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories;

/// <summary>
/// Task-specific repository with custom query methods.
/// Inherits all basic CRUD from GenericRepository and adds
/// domain-specific queries like filtering by status or category.
/// </summary>
public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
{
    public TaskRepository(TaskFlowDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status)
    {
        return await _context.Tasks
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TaskItem>> GetTasksByCategoryAsync(int categoryId)
    {
        return await _context.Tasks
            .Where(t => t.CategoryId == categoryId)
            .OrderByDescending(t => t.Priority)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskWithCategoryAsync(int id)
    {
        return await _context.Tasks
            .Include(t => t.Category)  // Eager load the related Category
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
