using OrdersApi.Contracts.Responses.Abstract;

namespace OrdersApi.Contracts.Responses;

public class OrderResponse : ResponseBase
{
    public ProductResponse Product { get; set; }
    public CustomerForOrderResponse Customer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Delivered { get; set; }
}