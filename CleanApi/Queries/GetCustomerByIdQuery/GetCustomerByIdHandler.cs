using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using CleanApi.Mappers;
using CleanApi.Repositories.Abstract;
using MediatR;

namespace CleanApi.Queries.GetCustomerByIdQuery;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse?>
{
    private readonly IRepository<CustomerEntity> _customerRepository;
    public GetCustomerByIdHandler(IRepository<CustomerEntity> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerResponse?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        return customer == null ? null : customer.ToResponse();
    }
}