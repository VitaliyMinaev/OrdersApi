using CleanApi.Entities.Abstract;

namespace CleanApi.Repositories.Abstract;

public interface IRepository<T>
    where T : EntityBase
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> AddAsync(T item, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T item, CancellationToken cancellationToken);
}