using CleanApi.Contracts.Responses;
using MediatR;

namespace CleanApi.Queries.GetOrderByIdQuery;

public class GetOrderByIdQuery : IRequest<OrderResponse?>
{
    public GetOrderByIdQuery(Guid orderId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; }
}