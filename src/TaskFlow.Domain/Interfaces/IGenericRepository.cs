namespace TaskFlow.Domain.Interfaces;

/// <summary>
/// Generic repository interface defining standard CRUD operations.
/// This follows the Repository Pattern, providing an abstraction over
/// data access that makes the domain layer independent of any ORM or database.
/// </summary>
/// <typeparam name="T">The entity type this repository manages.</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The entity's ID.</param>
    /// <returns>The entity if found; null otherwise.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all entities of this type.
    /// </summary>
    /// <returns>A read-only list of all entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Adds a new entity to the data store.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity with any generated fields populated.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the data store.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    Task DeleteAsync(T entity);
}
