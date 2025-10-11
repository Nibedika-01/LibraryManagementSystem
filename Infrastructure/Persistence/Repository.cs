using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Infrastructure.Persistence;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly LibraryDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(LibraryDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity with id {id} not found");
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = _dbSet;
        if(predicate != null) query = query.Where(predicate);
        return await query.ToListAsync();
    }

    public async Task AddAsync(T Entity)
    {
        await _dbSet.AddAsync(Entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T Entity)
    {
        _dbSet.Update(Entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
