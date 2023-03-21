using FluentResults;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Mappers;
using OrdersApi.Models;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.UpdateProductCommand;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result<ProductModel>>
{
    private readonly IRepository<ProductEntity> _productRepository;
    public UpdateProductHandler(IRepository<ProductEntity> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductEntity
        {
            Id = request.ProductId,
            Name = request.Name,
            Price = request.Price,
            ReleaseDate = request.ReleaseDate
        };

        var result = await _productRepository.UpdateAsync(entity, cancellationToken);

        if (result == false)
            return Result.Fail(new Error("Can not update product's data", new Error("Initial server error")));

        return Result.Ok(entity.ToModel());
    }
}