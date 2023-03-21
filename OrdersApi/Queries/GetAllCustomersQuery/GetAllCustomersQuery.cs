using MediatR;
using OrdersApi.Contracts.Responses;

namespace OrdersApi.Queries.GetAllCustomersQuery;

public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerModel>>
{  }