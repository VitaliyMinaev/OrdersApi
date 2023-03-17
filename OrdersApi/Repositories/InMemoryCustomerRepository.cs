using System.ComponentModel;
using OrdersApi.Entities;
using OrdersApi.Persistence;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Repositories;

public class InMemoryCustomerRepository : IRepository<CustomerEntity>
{
    private List<CustomerEntity> _customers;
    public InMemoryCustomerRepository()
    {
        var dataPrep = DataPreparations.GetInstance();
        _customers = dataPrep.Customers;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        return _customers;
    }

    public async Task<CustomerEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.Delay(200, cancellationToken);
        return _customers.FirstOrDefault(x => x.Id == id);
    }

    public async Task<bool> AddAsync(CustomerEntity item, CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken);
        _customers.Add(item);
        return true;
    }

    public async Task<bool> UpdateAsync(CustomerEntity item, CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken);
        var toUpdate = _customers.FirstOrDefault(x => x.Id == item.Id);
        if (toUpdate is null)
            return false;
        toUpdate.FullName = item.FullName;
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken);
        int result = _customers.RemoveAll(x => x.Id == id);
        return result > 0;
    }
}