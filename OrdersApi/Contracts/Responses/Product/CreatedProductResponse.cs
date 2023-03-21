namespace OrdersApi.Contracts.Responses.Product;

public class CreatedProductResponse 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}