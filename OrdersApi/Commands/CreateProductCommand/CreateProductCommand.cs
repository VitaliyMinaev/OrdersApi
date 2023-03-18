using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.CreateProductCommand;

public class CreateProductCommand : ICommand<ProductResponse>
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
