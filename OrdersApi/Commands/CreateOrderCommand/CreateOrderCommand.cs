using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;
using OrdersApi.Models;

namespace OrdersApi.Commands.CreateOrderCommand;

public class CreateOrderCommand : ICommand<OrderModel>
{
    public Guid ProductId { get; }
    public Guid CustomerId { get; }

    public CreateOrderCommand(Guid productId, Guid customerId)
    {
        ProductId = productId;
        CustomerId = customerId;
    }
}