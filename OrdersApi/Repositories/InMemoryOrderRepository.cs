using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Persistence;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Repositories;

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

        exist.DeliveryDate = order.DeliveryDate;
        exist.Delivered = order.Delivered;
        return true;
    }
    
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken);
        int result = _orders.RemoveAll(x => x.Id == id);
        return result > 0;
    }
}