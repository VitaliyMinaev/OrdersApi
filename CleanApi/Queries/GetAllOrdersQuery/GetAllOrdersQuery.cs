using CleanApi.Contracts.Responses;
using MediatR;

namespace CleanApi.Queries.GetAllOrdersQuery;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderResponse>>
{ }