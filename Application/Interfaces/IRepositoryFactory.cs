using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces;

public interface IRepositoryFactory
{
    IRepository<T> GetRepository<T>() where T : BaseEntity;
}