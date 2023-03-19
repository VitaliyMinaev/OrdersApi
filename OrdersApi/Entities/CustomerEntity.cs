using System.ComponentModel.DataAnnotations;
using OrdersApi.Entities.Abstract;

namespace OrdersApi.Entities;

public class CustomerEntity : BaseEntity
{
    [Required]
    public string FullName { get; set; }
    public ICollection<OrderEntity>? Orders { get; set; }
}