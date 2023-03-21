using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Models;

namespace OrdersApi.Queries.GetOrderByIdQuery;

public class GetOrderByIdQuery : IRequest<OrderModel?>
{
    public GetOrderByIdQuery(Guid orderId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; }
}