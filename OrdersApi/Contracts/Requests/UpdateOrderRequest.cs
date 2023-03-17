using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Contracts.Requests;

public class UpdateOrderRequest
{
    [Required] 
    public bool Delivered { get; set; }
}