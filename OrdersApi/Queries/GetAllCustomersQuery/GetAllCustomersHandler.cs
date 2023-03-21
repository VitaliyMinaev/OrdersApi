using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Mappers;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Queries.GetAllCustomersQuery;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerModel>>
{
    private readonly IRepository<CustomerEntity> _customerRepository;
    public GetAllCustomersHandler(IRepository<CustomerEntity> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return (await _customerRepository.GetAllAsync(cancellationToken)).Select(x => x.ToModel());
    }
}