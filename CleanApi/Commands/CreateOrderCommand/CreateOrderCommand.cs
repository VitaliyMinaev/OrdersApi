using CleanApi.Contracts.Responses;
using CleanApi.Messaging.Abstract;
using FluentResults;
using MediatR;

namespace CleanApi.Commands.CreateOrderCommand;

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