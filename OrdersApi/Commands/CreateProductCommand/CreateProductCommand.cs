using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;
using OrdersApi.Models;

namespace OrdersApi.Commands.CreateProductCommand;

public class CreateProductCommand : ICommand<ProductModel>
{
    public string Name { get; }
    public decimal Price { get; }
    public DateTime ReleaseDate { get; }
    public CreateProductCommand(string name, decimal price, DateTime releaseDate)
    {
        Name = name;
        Price = price;
        ReleaseDate = releaseDate;
    }
}
