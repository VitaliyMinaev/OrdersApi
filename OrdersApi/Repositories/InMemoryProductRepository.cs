using OrdersApi.Entities;
using OrdersApi.Persistence;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Repositories;

public class InMemoryProductRepository : IRepository<ProductEntity>
{
    private List<ProductEntity> _products;

    public InMemoryProductRepository()
    {
        var dataprep = DataPreparations.GetInstance();
        _products = dataprep.Products;
    }
    
    public async Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        return _products;
    }

    public async Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        return _products.FirstOrDefault(x => x.Id == id);
    }

    public async Task<bool> AddAsync(ProductEntity item, CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken);
        _products.Add(item);
        return true;
    }

    public async Task<bool> UpdateAsync(ProductEntity item, CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken);
        
        var exists = _products.FirstOrDefault(x => x.Id == item.Id);
        if (exists is null)
            return false;
        
        exists.Name = item.Name;
        exists.Price = item.Price;
        exists.ReleaseDate = item.ReleaseDate;
        return true;
    }
    
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken);
        int result = _products.RemoveAll(x => x.Id == id);
        return result > 0;
    }
}