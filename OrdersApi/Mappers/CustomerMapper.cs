using Domain;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Models;

namespace OrdersApi.Mappers;

public static class CustomerMapper
{
    public static CustomerModel ToModel(this CustomerEntity entity)
    {
        return new CustomerModel
        {
            Id = entity.Id,
            FullName = entity.FullName,
            OrdersId = entity.Orders?.Select(x => x.Id).ToList()
        };
    }
    public static CustomerForOrderModel ToOrderModel(this CustomerDomain domain)
    {
        return new CustomerForOrderModel
        {
            Id = domain.Id,
            FullName = domain.FullName
        };
    }
    public static CustomerForOrderModel ToOrderModel(this CustomerEntity entity)
    {
        return new CustomerForOrderModel
        {
            Id = entity.Id,
            FullName = entity.FullName
        };
    }
    public static CustomerDomain ToDomain(this CustomerEntity entity)
    {
        return new CustomerDomain(entity.Id, entity.FullName);
    }
}