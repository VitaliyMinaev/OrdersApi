using CleanApi.Contracts.Responses;
using CleanApi.Entities;

namespace CleanApi.Mappers;

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
    public static ProductEntity ToEntity(this ProductResponse response)
    {
        return new ProductEntity
        {
            Id = response.Id,
            ReleaseDate = response.ReleaseDate,
            Price = response.Price,
            Name = response.Name
        };
    }
}