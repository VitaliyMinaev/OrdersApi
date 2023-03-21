using FluentResults;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Mappers;
using OrdersApi.Models;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.CreateProductCommand;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<ProductModel>>
{
    private readonly IRepository<ProductEntity> _productRepository;
    public CreateProductHandler(IRepository<ProductEntity> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            ReleaseDate = request.ReleaseDate
        };

        var result = await _productRepository.AddAsync(entity, cancellationToken);

        if (result == false)
            return Result.Fail(new Error("Can not add product", new Error("Initial server error")));

        return entity.ToModel();
    }
}
