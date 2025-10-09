using LibraryManagementSystem.Domain.Entities;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
    Task AddAsync (T entity);
    Task UpdateAsync (T entity);
    Task DeleteAsync (int id);
}
