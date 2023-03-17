using FluentResults;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Mappers;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.UpdateCustomerCommand;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Result<CustomerResponse>>
{
    private readonly IRepository<CustomerEntity> _customerRepository;
    public UpdateCustomerHandler(IRepository<CustomerEntity> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<CustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new CustomerEntity
        {
            Id = request.CustomerId,
            FullName = request.FullName
        };

        var result = await _customerRepository.UpdateAsync(entity, cancellationToken);
        if (result == false)
            return Result.Fail(new Error("Can not update customer", new Error("Internal server error")));

        return entity.ToResponse();
    }
}