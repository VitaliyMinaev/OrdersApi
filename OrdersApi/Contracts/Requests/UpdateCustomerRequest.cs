using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Contracts.Requests;

public class UpdateCustomerRequest
{
    [Required]
    public string FullName { get; set; }
}