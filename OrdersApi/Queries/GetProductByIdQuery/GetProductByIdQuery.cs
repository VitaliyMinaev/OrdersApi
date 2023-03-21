using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Models;

namespace OrdersApi.Queries.GetProductByIdQuery;

public class GetProductByIdQuery : IRequest<ProductModel?>
{
    public Guid ProductId { get; }
    public GetProductByIdQuery(Guid productId)
    {
        ProductId = productId;
    }
}
