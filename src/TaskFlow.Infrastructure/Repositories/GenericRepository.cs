using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation using Entity Framework Core.
/// Provides base CRUD operations that all entity repositories inherit.
/// 
/// This eliminates repetitive data access code — you write the basic
/// operations once and reuse them across all entities.
/// </summary>
/// <typeparam name="T">The entity type this repository manages.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TaskFlowDbContext _context;

    public GenericRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
