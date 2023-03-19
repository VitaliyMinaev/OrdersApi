using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Entities;
using OrdersApi.Persistence;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Repositories;

public class ProductRepository : IRepository<ProductEntity>
{
    private readonly ApplicationDatabaseContext _databaseContext;
    public ProductRepository(ApplicationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> AddAsync(ProductEntity item, CancellationToken cancellationToken)
    {
        await _databaseContext.Products.AddAsync(item, cancellationToken);
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _databaseContext.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (exists is null)
            throw new ArgumentException("There is no product with given id");
        
        _databaseContext.Products.Remove(exists);
        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<ProductEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _databaseContext.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _databaseContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ProductEntity item, CancellationToken cancellationToken)
    {
        var toUpdate = await _databaseContext.Products.FirstOrDefaultAsync(x => x.Id == item.Id, cancellationToken);

        if (toUpdate == null)
            throw new ArgumentException("There is no product with given id");

        toUpdate.Name = item.Name;
        toUpdate.Price = item.Price;
        toUpdate.ReleaseDate = item.ReleaseDate;

        return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
