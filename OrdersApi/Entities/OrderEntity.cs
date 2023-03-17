using OrdersApi.Contracts.Responses;
using OrdersApi.Entities.Abstract;

namespace OrdersApi.Entities;

public class OrderEntity : BaseEntity
{
    public ProductEntity Product { get; set; }
    public CustomerEntity Customer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Delivered { get; set; }
}