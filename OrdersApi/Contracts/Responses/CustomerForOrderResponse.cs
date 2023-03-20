using OrdersApi.Contracts.Responses.Abstract;

namespace OrdersApi.Contracts.Responses;

public class CustomerForOrderResponse : ResponseBase
{
    public string FullName { get; set; }
}