using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using CleanApi.Repositories.Abstract;

namespace CleanApi.Repositories;

public class InMemoryOrderRepository : IRepository<OrderEntity>
{
    private List<OrderEntity> _orders;
    public InMemoryOrderRepository()
    {
        _orders = Enumerable.Range(0, Random.Shared.Next(1, 4)).Select(x => new OrderEntity()
        {
            Id = Guid.NewGuid(),
            Customer = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                FullName = string.Empty
            },
            Product = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Price = (decimal)Random.Shared.Next(100, 600),
                ReleaseDate = DateTime.Now.AddDays(-Random.Shared.Next(14, 41))
            },
            Delivered = false,
            DeliveryDate = DateTime.Now.AddDays(Random.Shared.Next(7, 14))
        }).ToList();
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