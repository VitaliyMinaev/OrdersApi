using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using CleanApi.Persistence;
using CleanApi.Repositories.Abstract;

namespace CleanApi.Repositories;

public class InMemoryOrderRepository : IRepository<OrderEntity>
{
    private List<OrderEntity> _orders;
    public InMemoryOrderRepository()
    {
        var data = DataPreparations.GetInstance();
        _orders = data.Orders;
    }

    public async Task<IEnumerable<OrderEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(500);
        return _orders;
    }

    public async Task<OrderEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        return _orders.FirstOrDefault(x => x.Id == id);
    }

    public async Task<bool> AddAsync(OrderEntity order, CancellationToken cancellationToken)
    {
        if (order == null)
            throw new NullReferenceException();
        
        await Task.Delay(400);
        
        _orders.Add(order);
        return true;
    }

    public async Task<bool> UpdateAsync(OrderEntity order, CancellationToken cancellationToken)
    {
        var exist = await GetByIdAsync(order.Id, cancellationToken);

        if (exist is null)
            return false;

        exist = order;
        return true;
    }
}