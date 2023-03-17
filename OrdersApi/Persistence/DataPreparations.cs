using OrdersApi.Entities;

namespace OrdersApi.Persistence;

public class DataPreparations
{
    private static DataPreparations? _instance = null;
    private List<ProductEntity> _products;
    private List<CustomerEntity> _customers;
    private List<OrderEntity> _orders;
    private DataPreparations()
    {
        _products = Enumerable.Range(1, Random.Shared.Next(3, 8)).Select(x => new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = GetRandomProductName(),
            Price = (decimal)Random.Shared.Next(100, 600),
            ReleaseDate = DateTime.Now.AddDays(-Random.Shared.Next(14, 89))
        }).ToList();
        _customers = Enumerable.Range(1, Random.Shared.Next(4, 7)).Select(x => 
            new CustomerEntity
            {
                Id = Guid.NewGuid(), 
                FullName = GetRandomFullname()
            }).ToList();
        _orders = Enumerable.Range(0, Random.Shared.Next(4, 8)).Select(x => new OrderEntity()
        {
            Id = Guid.NewGuid(),
            Customer = _customers[Random.Shared.Next(0, _customers.Count)],
            Product = _products[Random.Shared.Next(0, _products.Count)],
            Delivered = false,
            DeliveryDate = DateTime.Now.AddDays(Random.Shared.Next(7, 14))
        }).ToList();
    }

    public static DataPreparations GetInstance()
    {
        if (_instance is null)
            _instance = new DataPreparations();

        return _instance;
    }

    private static string GetRandomProductName()
    {
        var productNames = new string[]
        {
            "ElectraZap", "NexaMaxx", "VoltMender", "SparkWave", "CircuitMaster", "TechTonic", "WaveSonic",
            "ElectroPulse", "PulsePro", "ShockBlast", "AmpJuice", "VoltaPrime", "PowerXtreme", "ElectraSmith"
        };

        return productNames[Random.Shared.Next(0, productNames.Length)];
    }

    private static string GetRandomFullname()
    {
        var fullNames = new string[]
        {
            "Emily Rodriguez", "Brandon Lee", "Samantha Campbell", "Tyler Nguyen", "Maria Hernandez", "Adam Patel", "Jasmine Kim",
            "Christopher Jones", "Rachel Davis", "Daniel Smith", "Ava Brown", "Ethan Garcia", "Olivia Taylor", "Lucas Martinez",
            "Madison Mitchell", "Anthony Wright", "Grace Lee", "Benjamin Johnson", "Natalie Clark", "Jacob Williams"
        };

        return fullNames[Random.Shared.Next(0, fullNames.Length)];
    }

    public List<ProductEntity> Products
    {
        get => _products;
    }
    public List<CustomerEntity> Customers
    {
        get => _customers;
    }
    public List<OrderEntity> Orders
    {
        get => _orders;
    }
}