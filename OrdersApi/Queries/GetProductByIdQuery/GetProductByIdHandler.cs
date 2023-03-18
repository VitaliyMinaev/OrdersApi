using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Mappers;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Queries.GetProductByIdQuery;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse?>
{
    private readonly IRepository<ProductEntity> _productRepository;
    public GetProductByIdHandler(IRepository<ProductEntity> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return (await _productRepository.GetByIdAsync(request.ProductId, cancellationToken))?.ToResponse();
    }
}
