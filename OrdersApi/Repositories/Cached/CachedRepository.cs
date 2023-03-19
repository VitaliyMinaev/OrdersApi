using OrdersApi.Entities;
using Microsoft.Extensions.Caching.Memory;
using OrdersApi.Entities.Abstract;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Repositories.Cached;

public class CachedRepository<T> : IRepository<T>
    where T : BaseEntity
{
    private readonly IRepository<T> _orderRepository;
    private readonly IMemoryCache _cache;
    public CachedRepository(IRepository<T> orderRepository, IMemoryCache cache)
    {
        _orderRepository = orderRepository;
        _cache = cache;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<T>? orders = null;
        if (_cache.TryGetValue(CacheKeys.GetAll, out orders))
        {
            return orders;
        }

        orders = (await _orderRepository.GetAllAsync(cancellationToken)).ToList();
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(15));
        _cache.Set(CacheKeys.GetAll, orders, cacheEntryOptions);
        return orders;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        T? order = null;
        if (_cache.TryGetValue(CacheKeys.GetById.Replace("{id}", id.ToString()), out order))
        {
            return order;
        }

        order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
        _cache.Set(CacheKeys.GetById.Replace("{id}", id.ToString()), order, cacheEntryOptions);
        return order;
    }

    public async Task<bool> AddAsync(T item, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.AddAsync(item, cancellationToken);
        if (result == false)
            return false;
        
        _cache.Remove(CacheKeys.GetAll);
        return true;
    }

    public async Task<bool> UpdateAsync(T item, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.UpdateAsync(item, cancellationToken);
        if (result == false)
            return false;

        RemoveItemFromCache(item.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.DeleteAsync(id, cancellationToken);

        if (result == false)
            return false;

        RemoveItemFromCache(id);
        return true;
    }

    private void RemoveItemFromCache(Guid id)
    {
        _cache.Remove(CacheKeys.GetAll);
        _cache.Remove(CacheKeys.GetById.Replace("{id}", id.ToString()));
    }
}