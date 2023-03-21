using Domain;
using OrdersApi.Entities;
using OrdersApi.Models;

namespace OrdersApi.Mappers;

public static class OrderMapper
{
    public static OrderModel ToModel(this OrderEntity entity)
    {
        return new OrderModel
        {
            Id = entity.Id,
            Customer = entity.Customer.ToOrderModel(),
            Product = entity.Product.ToModel(),
            Delivered = entity.Delivered,
            DeliveryDate = entity.DeliveryDate
        };
    }
    public static OrderModel ToModel(this OrderDomain domain)
    {
        return new OrderModel
        {
            Id = domain.Id,
            Customer = domain.Customer.ToOrderModel(),
            Product = domain.Product.ToModel(),
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