using CleanApi.Entities;
using CleanApi.Persistence;
using CleanApi.Repositories.Abstract;

namespace CleanApi.Repositories;

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
        
        exists = item;
        return true;
    }
}