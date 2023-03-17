using OrdersApi.Contracts.Responses.Abstract;

namespace OrdersApi.Contracts.Responses;

public class ProductResponse : ResponseBase
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}