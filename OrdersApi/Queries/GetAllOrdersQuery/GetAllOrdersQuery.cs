using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Models;

namespace OrdersApi.Queries.GetAllOrdersQuery;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderModel>>
{ }