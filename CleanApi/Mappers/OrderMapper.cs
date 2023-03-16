using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using Domain;

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
    public static OrderResponse ToResponse(this OrderDomain domain)
    {
        return new OrderResponse
        {
            Id = domain.Id,
            Customer = domain.Customer.ToResponse(),
            Product = domain.Product.ToResponse(),
            Delivered = domain.Delivered,
            DeliveryDate = domain.DeliveryDate
        };
    }

    public static OrderEntity ToEntity(this OrderResponse response)
    {
        return new OrderEntity
        {
            Id = response.Id
        };
    }

    public static OrderDomain ToDomain(this OrderEntity entity)
    {
        return new OrderDomain(entity.Id, entity.Customer.ToDomain(), entity.Product.ToDomain(), entity.DeliveryDate, entity.Delivered);
    }

    public static OrderEntity ToEntity(this OrderDomain domain)
    {
        return new OrderEntity
        {
            Id = domain.Id,
            Customer = domain.Customer.ToEntity(),
            Product = domain.Product.ToEntity(),
            DeliveryDate = domain.DeliveryDate,
            Delivered = domain.Delivered
        };
    }
        
}