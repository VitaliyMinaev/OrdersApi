using Microsoft.EntityFrameworkCore;
using OrdersApi.Entities;
using OrdersApi.Persistence;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Repositories;

public class OrderRepository : IRepository<OrderEntity>
{
    private readonly ApplicationDatabaseContext _databaseContext;
    public OrderRepository(ApplicationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<OrderEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _databaseContext.Orders.Include(x => x.Customer).Include(x => x.Product)
            .AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task<OrderEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _databaseContext.Orders.AsNoTracking().Include(x => x.Customer)
            .Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> AddAsync(OrderEntity item, CancellationToken cancellationToken)
    {
        await _databaseContext.Orders.AddAsync(item, cancellationToken);
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(OrderEntity item, CancellationToken cancellationToken)
    {
        var toUpdate = await _databaseContext.Orders.FirstOrDefaultAsync(x => x.Id == item.Id, cancellationToken);

        if (toUpdate is null)
            throw new ArgumentException("There is no customer with given id");

        toUpdate.Delivered = item.Delivered;
        toUpdate.DeliveryDate = item.DeliveryDate;
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _databaseContext.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (exists is null)
            throw new ArgumentException("There is no order with given id");
        
        _databaseContext.Orders.Remove(exists);
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }
}