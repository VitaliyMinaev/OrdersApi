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
            Customer = entity.Customer.ToResponse(),
            Product = entity.Product.ToResponse(),
            Delivered = entity.Delivered,
            DeliveryDate = entity.DeliveryDate
        };
    }

    public static OrderEntity ToEntity(this OrderResponse response)
    {
        return new OrderEntity
        {
            Id = response.Id
        };
    }
}