using CleanApi.Contracts.Responses.Abstract;

namespace CleanApi.Contracts.Responses;

public class OrderResponse : ResponseBase
{
    public ProductResponse Product { get; set; }
    public CustomerResponse Customer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool Delivered { get; set; }
}