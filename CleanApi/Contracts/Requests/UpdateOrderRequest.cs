using System.ComponentModel.DataAnnotations;

namespace CleanApi.Contracts.Requests;

public class UpdateOrderRequest
{
    [Required] 
    public bool Delivered { get; set; }
}