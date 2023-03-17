using System.ComponentModel.DataAnnotations;

namespace CleanApi.Contracts.Requests;

public class CreateCustomerRequest
{
    [Required] 
    public string FullName { get; set; }
}