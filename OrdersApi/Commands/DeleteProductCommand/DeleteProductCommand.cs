using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.DeleteProductCommand;

public class DeleteProductCommand : ICommand
{
    public Guid ProductId { get; }
    public DeleteProductCommand(Guid productId)
    {
        ProductId = productId;
    }
}