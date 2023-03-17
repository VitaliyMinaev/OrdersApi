using System.Collections.ObjectModel;
using Domain.Abstract;

namespace Domain;

public class CustomerDomain : BaseDomain
{
    public string FullName { get; private set; }
    private List<OrderDomain> _orders;
    public CustomerDomain(Guid id, string fullName) : base(id)
    {
        FullName = fullName;
        _orders = new();
    }
    public CustomerDomain(Guid id, string fullName, IEnumerable<OrderDomain> orders) : base(id)
    {
        FullName = fullName;
        _orders = orders.ToList();
    }

    public void RenameUser(string fullName)
    {
        FullName = fullName;
    }

    public IReadOnlyCollection<OrderDomain> Orders
    {
        get => new ReadOnlyCollection<OrderDomain>(_orders);
    }

    public OrderDomain OrderProduct(ProductDomain product)
    {
        var order = new OrderDomain(Guid.NewGuid(), this, product, CalculateDeliveryTime(), false);
        _orders.Add(order);
        return order;
    }

    public DateTime CalculateDeliveryTime()
    {
        // Some complex business logic
        return DateTime.Now.AddDays(Random.Shared.Next(1, 8));
    }
}