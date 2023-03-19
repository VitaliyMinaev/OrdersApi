using Domain;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;

namespace OrdersApi.Mappers;

/*
 * {
  "productId": "54048af0-a0ac-444f-af53-e9056898343b",
  "customerId": "41cc5ac2-3ef7-4e10-a157-5a3e1cfc56e5"
}
 */
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