using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Mappers;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Queries.GetAllProductsQuery;

public class GetProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
{
    private readonly IRepository<ProductEntity> _productRepository;
    public GetProductsHandler(IRepository<ProductEntity> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return (await _productRepository.GetAllAsync(cancellationToken)).Select(x => x.ToResponse());
    }
}
