using CleanApi.Entities;

namespace CleanApi.Repositories.Abstract;

public interface IOrderRepository
{
    Task<IEnumerable<OrderEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<OrderEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> AddAsync(OrderEntity order, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(OrderEntity order, CancellationToken cancellationToken);
}