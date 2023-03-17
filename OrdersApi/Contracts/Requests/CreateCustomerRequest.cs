using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Contracts.Requests;

public class CreateCustomerRequest
{
    [Required] 
    public string FullName { get; set; }
}