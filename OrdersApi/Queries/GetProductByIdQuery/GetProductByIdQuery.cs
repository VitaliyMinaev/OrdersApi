using MediatR;
using OrdersApi.Contracts.Responses;

namespace OrdersApi.Queries.GetProductByIdQuery;

public class GetProductByIdQuery : IRequest<ProductResponse?>
{
    public Guid ProductId { get; }
    public GetProductByIdQuery(Guid productId)
    {
        ProductId = productId;
    }
}
