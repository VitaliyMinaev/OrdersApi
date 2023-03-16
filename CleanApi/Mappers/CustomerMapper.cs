using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using Domain;

namespace CleanApi.Mappers;

public static class CustomerMapper
{
    public static CustomerResponse ToResponse(this CustomerEntity entity)
    {
        return new CustomerResponse
        {
            Id = entity.Id,
            FullName = entity.FullName
        };
    }

    public static CustomerResponse ToResponse(this CustomerDomain domain)
    {
        return new CustomerResponse
        {
            Id = domain.Id,
            FullName = domain.FullName
        };
    }

    public static CustomerEntity ToEntity(this CustomerResponse response)
    {
        return new CustomerEntity
        {
            Id = response.Id,
            FullName = response.FullName
        };
    }
    public static CustomerEntity ToEntity(this CustomerDomain domain)
    {
        return new CustomerEntity
        {
            Id = domain.Id,
            FullName = domain.FullName
        };
    }

    public static CustomerDomain ToDomain(this CustomerEntity entity)
    {
        return new CustomerDomain(entity.Id, entity.FullName);
    }
}