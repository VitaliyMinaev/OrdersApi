using CleanApi.Contracts.Responses;
using CleanApi.Entities;

namespace CleanApi.Mappers;

public static class OrderMapper
{
    public static OrderResponse ToResponse(this OrderEntity entity)
    {
        return new OrderResponse
        {
            Id = entity.Id,
            Customer = new CustomerResponse
            {
                Id = entity.Customer.Id,
                FullName = entity.Customer.FullName
            },
            Product = new ProductResponse
            {
                Id = entity.Product.Id,
                Name = entity.Product.Name,
                Price = entity.Product.Price,
                ReleaseDate = entity.Product.ReleaseDate
            },
            Delivered = entity.Delivered,
            DeliveryDate = entity.DeliveryDate
        };
    }
}