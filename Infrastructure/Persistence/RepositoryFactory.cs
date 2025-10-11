using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Infrastructure.Persistence;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly LibraryDbContext _context;

    public RepositoryFactory(LibraryDbContext context)
    {
        _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new Repository<T>(_context);
    }
}