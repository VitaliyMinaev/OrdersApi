using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.DeleteOrderCommand;

public class DeleteOrderCommand : ICommand
{
    public Guid OrderId { get; }
    public DeleteOrderCommand(Guid orderId)
    {
        OrderId = orderId;
    }
}