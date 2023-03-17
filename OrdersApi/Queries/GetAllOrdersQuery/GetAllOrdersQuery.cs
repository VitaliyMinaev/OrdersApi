using MediatR;
using OrdersApi.Contracts.Responses;

namespace OrdersApi.Queries.GetAllOrdersQuery;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderResponse>>
{ }