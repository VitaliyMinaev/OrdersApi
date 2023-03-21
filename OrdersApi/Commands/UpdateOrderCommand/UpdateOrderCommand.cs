using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;
using OrdersApi.Models;

namespace OrdersApi.Commands.UpdateOrderCommand;

public class UpdateOrderCommand : ICommand<OrderModel>
{
    public Guid OrderId { get; }
    public bool Delivered { get; }

    public UpdateOrderCommand(Guid orderId, bool delivered)
    {
        OrderId = orderId;
        Delivered = delivered;
    }
}