using Domain;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;

namespace OrdersApi.Mappers;

public static class CustomerMapper
{
    public static CustomerResponse ToResponse(this CustomerEntity entity)
    {
        return new CustomerResponse
        {
            Id = entity.Id,
            FullName = entity.FullName,
            OrdersId = entity.Orders?.Select(x => x.Id).ToList()
        };
    }
    public static CustomerForOrderResponse ToOrderResponse(this CustomerDomain domain)
    {
        return new CustomerForOrderResponse
        {
            Id = domain.Id,
            FullName = domain.FullName
        };
    }
    public static CustomerForOrderResponse ToOrderResponse(this CustomerEntity entity)
    {
        return new CustomerForOrderResponse
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