using CleanApi.Entities;
using CleanApi.Repositories.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace CleanApi.Repositories.Cached;

public class OrderCachedRepository : IRepository<OrderEntity>, IDisposable
{
    private readonly IRepository<OrderEntity> _orderRepository;
    private readonly IMemoryCache _cache;
    public OrderCachedRepository(IRepository<OrderEntity> orderRepository, IMemoryCache cache)
    {
        _orderRepository = orderRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<OrderEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<OrderEntity>? orders = null;
        if (_cache.TryGetValue(CacheKeys.GetAll, out orders))
        {
            return orders;
        }

        orders = (await _orderRepository.GetAllAsync(cancellationToken)).ToList();
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(15));
        _cache.Set(CacheKeys.GetAll, orders, cacheEntryOptions);
        return orders;
    }

    public async Task<OrderEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        OrderEntity? order = null;
        if (_cache.TryGetValue(CacheKeys.GetById.Replace("{id}", id.ToString()), out order))
        {
            return order;
        }

        order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
        _cache.Set(CacheKeys.GetById.Replace("{id}", id.ToString()), order, cacheEntryOptions);
        return order;
    }

    public async Task<bool> AddAsync(OrderEntity item, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.AddAsync(item, cancellationToken);
        if (result == false)
            return false;
        
        _cache.Remove(CacheKeys.GetAll);
        return true;
    }

    public async Task<bool> UpdateAsync(OrderEntity item, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.UpdateAsync(item, cancellationToken);
        if (result == false)
            return false;
        
        _cache.Remove(CacheKeys.GetAll);
        _cache.Remove(CacheKeys.GetById.Replace("{id}", item.Id.ToString()));
        return true;
    }

    public void Dispose()
    {
        _cache.Dispose();
    }
}