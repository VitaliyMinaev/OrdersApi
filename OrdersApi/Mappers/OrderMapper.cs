using Domain;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;

namespace OrdersApi.Mappers;

public static class OrderMapper
{
    public static OrderResponse ToResponse(this OrderEntity entity)
    {
        return new OrderResponse
        {
            Id = entity.Id,
            Customer = entity.Customer.ToOrderResponse(),
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
            Customer = domain.Customer.ToOrderResponse(),
            Product = domain.Product.ToResponse(),
            Delivered = domain.Delivered,
            DeliveryDate = domain.DeliveryDate
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
            CustomerId = domain.Customer.Id,
            ProductId = domain.Product.Id,
            DeliveryDate = domain.DeliveryDate,
            Delivered = domain.Delivered
        };
    }
}