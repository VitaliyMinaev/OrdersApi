using FluentResults;
using MediatR;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.DeleteProductCommand;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IRepository<ProductEntity> _productRepository;
    public DeleteProductHandler(IRepository<ProductEntity> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.DeleteAsync(request.ProductId, cancellationToken);
        
        if(result == false)
            return Result.Fail(new Error("Can not delete product", new Error("Initial server error")));
        
        return Result.Ok();
    }
}