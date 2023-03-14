using CleanApi.Contracts.Responses;
using CleanApi.Entities;

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

    public static CustomerEntity ToEntity(this CustomerResponse response)
    {
        return new CustomerEntity
        {
            Id = response.Id,
            FullName = response.FullName
        };
    }
}