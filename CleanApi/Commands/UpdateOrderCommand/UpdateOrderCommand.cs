using CleanApi.Contracts.Responses;
using MediatR;

namespace CleanApi.Commands.UpdateOrderCommand;

public class UpdateOrderCommand : IRequest<OrderResponse>
{
    public Guid OrderId { get; }
    public bool Delivered { get; }

    public UpdateOrderCommand(Guid orderId, bool delivered)
    {
        OrderId = orderId;
        Delivered = delivered;
    }
}