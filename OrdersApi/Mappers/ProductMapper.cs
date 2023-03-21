using Domain;
using OrdersApi.Contracts.Responses.Product;
using OrdersApi.Entities;
using OrdersApi.Models;

namespace OrdersApi.Mappers;

public static class ProductMapper
{
    public static ProductModel ToModel(this ProductEntity entity)
    {
        return new ProductModel
        {
            Id = entity.Id,
            ReleaseDate = entity.ReleaseDate,
            Price = entity.Price,
            Name = entity.Name
        };
    }
    public static ProductModel ToModel(this ProductDomain domain)
    {
        return new ProductModel
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