using OrdersApi.Contracts.Responses.Abstract;

namespace OrdersApi.Contracts.Responses;

public class CustomerResponse : ResponseBase
{
    public string FullName { get; set; }
}