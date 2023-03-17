using OrdersApi.Contracts.Responses;
using OrdersApi.Messaging.Abstract;

namespace OrdersApi.Commands.CreateOrderCommand;

public class CreateOrderCommand : ICommand<OrderResponse>
{
    public Guid ProductId { get; }
    public Guid CustomerId { get; }

    public CreateOrderCommand(Guid productId, Guid customerId)
    {
        ProductId = productId;
        CustomerId = customerId;
    }
}