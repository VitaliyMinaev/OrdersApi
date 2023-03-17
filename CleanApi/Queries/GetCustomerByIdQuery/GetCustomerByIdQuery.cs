using CleanApi.Contracts.Responses;
using MediatR;

namespace CleanApi.Queries.GetCustomerByIdQuery;

public class GetCustomerByIdQuery : IRequest<CustomerResponse?>
{
    public Guid Id { get; }    
    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}