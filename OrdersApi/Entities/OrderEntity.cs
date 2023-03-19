using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities.Abstract;

namespace OrdersApi.Entities;

public class OrderEntity : BaseEntity
{
    [ForeignKey(nameof(Product)), Required]
    public Guid ProductId { get; set; }
    public ProductEntity? Product { get; set; }
    [ForeignKey(nameof(Customer)), Required]
    public Guid CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }
    
    public DateTime DeliveryDate { get; set; }
    public bool Delivered { get; set; }
}