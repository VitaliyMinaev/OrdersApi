using MediatR;
using OrdersApi.Contracts.Responses;

namespace OrdersApi.Queries.GetAllProductsQuery;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductResponse>>
{
}
