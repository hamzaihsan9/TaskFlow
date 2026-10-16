using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories;

/// <summary>
/// Category-specific repository implementation.
/// </summary>
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(TaskFlowDbContext context) : base(context)
    {
    }

    public async Task<bool> CategoryExistsAsync(string name)
    {
        return await _context.Categories
            .AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }

    public async Task<Category?> GetCategoryWithTasksAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.Tasks)  // Eager load all tasks in this category
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
