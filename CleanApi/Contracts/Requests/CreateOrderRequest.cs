using System.ComponentModel.DataAnnotations;

namespace CleanApi.Contracts.Requests;

public class CreateOrderRequest
{
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
}