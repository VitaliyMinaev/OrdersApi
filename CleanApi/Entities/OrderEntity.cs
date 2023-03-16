using CleanApi.Contracts.Responses;
using CleanApi.Entities.Abstract;

namespace CleanApi.Entities;

public class OrderEntity : BaseEntity
{
    public ProductEntity Product { get; set; }
    public CustomerEntity Customer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Delivered { get; set; }
}