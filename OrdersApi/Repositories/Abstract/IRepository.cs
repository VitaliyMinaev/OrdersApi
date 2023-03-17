using OrdersApi.Entities.Abstract;

namespace OrdersApi.Repositories.Abstract;

public interface IRepository<T>
    where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> AddAsync(T item, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T item, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}