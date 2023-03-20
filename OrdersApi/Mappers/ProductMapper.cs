using Domain;
using OrdersApi.Contracts.Requests;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;

namespace OrdersApi.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToResponse(this ProductEntity entity)
    {
        return new ProductResponse
        {
            Id = entity.Id,
            ReleaseDate = entity.ReleaseDate,
            Price = entity.Price,
            Name = entity.Name
        };
    }
    public static ProductResponse ToResponse(this ProductDomain domain)
    {
        return new ProductResponse
        {
            Id = domain.Id,
            ReleaseDate = domain.ReleaseDate,
            Price = domain.Price,
            Name = domain.Name
        };
    }
    public static ProductDomain ToDomain(this ProductEntity entity)
    {
        return new ProductDomain(entity.Id, entity.Name, entity.Price, entity.ReleaseDate);
    }
}