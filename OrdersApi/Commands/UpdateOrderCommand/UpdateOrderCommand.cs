using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.UpdateOrderCommand;

public class UpdateOrderCommand : ICommand<OrderResponse>
{
    public Guid OrderId { get; }
    public bool Delivered { get; }

    public UpdateOrderCommand(Guid orderId, bool delivered)
    {
        OrderId = orderId;
        Delivered = delivered;
    }
}