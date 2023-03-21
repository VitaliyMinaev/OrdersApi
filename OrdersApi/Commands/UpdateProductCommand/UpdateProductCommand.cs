using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;
using OrdersApi.Models;

namespace OrdersApi.Commands.UpdateProductCommand;

public class UpdateProductCommand : ICommand<ProductModel>
{
    public Guid ProductId { get; }
    public string Name { get; }
    public decimal Price { get; }
    public DateTime ReleaseDate { get; }
    
    public UpdateProductCommand(Guid productId, string name, decimal price, DateTime releaseDate)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        ReleaseDate = releaseDate;
    }
}