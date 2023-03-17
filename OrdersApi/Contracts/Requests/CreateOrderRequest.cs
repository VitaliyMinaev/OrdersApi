using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Contracts.Requests;

public class CreateOrderRequest
{
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
}