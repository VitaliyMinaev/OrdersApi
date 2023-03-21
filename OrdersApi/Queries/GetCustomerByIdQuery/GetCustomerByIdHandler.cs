using OrdersApi.Mappers;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Queries.GetCustomerByIdQuery;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerModel?>
{
    private readonly IRepository<CustomerEntity> _customerRepository;
    public GetCustomerByIdHandler(IRepository<CustomerEntity> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        return customer == null ? null : customer.ToModel();
    }
}