using CleanApi.Contracts.Responses;
using MediatR;

namespace CleanApi.Commands.CreateOrderCommand;

public class CreateOrderCommand : IRequest<OrderResponse>
{
    public Guid ProductId { get; }
    public Guid CustomerId { get; }

    public CreateOrderCommand(Guid productId, Guid customerId)
    {
        ProductId = productId;
        CustomerId = customerId;
    }
}