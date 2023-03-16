using CleanApi.Contracts.Responses;
using CleanApi.Messaging.Abstract;
using MediatR;

namespace CleanApi.Commands.UpdateOrderCommand;

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