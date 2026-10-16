using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;

/// <summary>
/// Repository interface specific to Category entities.
/// Extends the generic repository with category-specific query methods.
/// </summary>
public interface ICategoryRepository : IGenericRepository<Category>
{
    /// <summary>
    /// Checks whether a category with the given name already exists.
    /// Used to enforce unique category names.
    /// </summary>
    /// <param name="name">The category name to check.</param>
    /// <returns>True if a category with this name exists.</returns>
    Task<bool> CategoryExistsAsync(string name);

    /// <summary>
    /// Retrieves a category by ID including all of its associated tasks.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <returns>The category with tasks loaded, or null.</returns>
    Task<Category?> GetCategoryWithTasksAsync(int id);
}
