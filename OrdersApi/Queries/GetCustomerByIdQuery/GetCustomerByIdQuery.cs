using MediatR;
using OrdersApi.Contracts.Responses;

namespace OrdersApi.Queries.GetCustomerByIdQuery;

public class GetCustomerByIdQuery : IRequest<CustomerModel?>
{
    public Guid Id { get; }    
    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}