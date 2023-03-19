using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Entities;
using OrdersApi.Persistence;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Repositories;

public class CustomerRepository : IRepository<CustomerEntity>
{
    private readonly ApplicationDatabaseContext _databaseContext;
    public CustomerRepository(ApplicationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _databaseContext.Customers.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<CustomerEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _databaseContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> AddAsync(CustomerEntity item, CancellationToken cancellationToken)
    {
        await _databaseContext.Customers.AddAsync(item, cancellationToken);
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(CustomerEntity item, CancellationToken cancellationToken)
    {
        var toUpdate = await _databaseContext.Customers.FirstOrDefaultAsync(x => x.Id == item.Id, cancellationToken);

        if (toUpdate == null)
            throw new ArgumentException("There is no customer with given id");

        toUpdate.FullName = item.FullName;
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _databaseContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (exists is null)
            throw new ArgumentException("There is no product with given id");
        
        _databaseContext.Customers.Remove(exists);
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }
}