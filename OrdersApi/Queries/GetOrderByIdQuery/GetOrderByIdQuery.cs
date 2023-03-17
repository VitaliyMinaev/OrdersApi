using MediatR;
using OrdersApi.Contracts.Responses;

namespace OrdersApi.Queries.GetOrderByIdQuery;

public class GetOrderByIdQuery : IRequest<OrderResponse?>
{
    public GetOrderByIdQuery(Guid orderId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; }
}