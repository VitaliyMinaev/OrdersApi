using OrdersApi.Entities;
using OrdersApi.Persistence.Abstract;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Persistence;

public class SqliteDataLoader : IDataLoader
{
    private readonly DataPreparations _data;
    
    private readonly IRepository<ProductEntity> _productRepository;
    private readonly IRepository<CustomerEntity> _customerRepository;
    private readonly IRepository<OrderEntity> _orderRepository;
    private readonly ILogger _logger;
    public SqliteDataLoader(IRepository<ProductEntity> productRepository, IRepository<CustomerEntity> customerRepository,
        IRepository<OrderEntity> orderRepository, ILogger<SqliteDataLoader> logger)
    {
        _data = DataPreparations.GetInstance();
        
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _logger = logger;
    }
    
    public async Task LoadDataAsync()
    {
        if((await _customerRepository.GetAllAsync(CancellationToken.None)).Any() == false)
            await AddCustomers(_data.Customers);
        if((await _productRepository.GetAllAsync(CancellationToken.None)).Any() == false)
            await AddProducts(_data.Products);
        if((await _orderRepository.GetAllAsync(CancellationToken.None)).Any() == false)
            await AddOrders(_data.Orders);
    }

    private async Task AddOrders(List<OrderEntity> orders)
    {
        foreach (var item in orders)
        {
            if (await _orderRepository.AddAsync(item, CancellationToken.None) == false)
            {
                _logger.LogWarning($"Failed to add order: {item.Id}");
            }
        }
    }

    private async Task AddProducts(List<ProductEntity> products)
    {
        foreach (var item in products)
        {
            if (await _productRepository.AddAsync(item, CancellationToken.None) == false)
            {
                _logger.LogWarning($"Failed to add product: {item.Name}");
            }
        }
    }

    private async Task AddCustomers(List<CustomerEntity> customers)
    {
        foreach (var item in customers)
        {
            if (await _customerRepository.AddAsync(item, CancellationToken.None) == false)
            {
                _logger.LogWarning($"Failed to add customer: {item.FullName}");
            }
        }
    }
}