using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Models;

namespace OrdersApi.Queries.GetAllProductsQuery;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductModel>>
{
}
