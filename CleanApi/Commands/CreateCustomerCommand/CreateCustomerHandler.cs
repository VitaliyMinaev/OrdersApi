using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using CleanApi.Mappers;
using CleanApi.Repositories.Abstract;
using FluentResults;
using MediatR;

namespace CleanApi.Commands.CreateCustomerCommand;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Result<CustomerResponse>>
{
    private readonly IRepository<CustomerEntity> _customerRepository;
    public CreateCustomerHandler(IRepository<CustomerEntity> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerEntity = new CustomerEntity
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName
        };

        var operationResult = await _customerRepository.AddAsync(customerEntity, cancellationToken);
        
        if (operationResult == false)
            return Result.Fail(new Error("Can not add customer", new Error("Internal server error")));
        return Result.Ok(customerEntity.ToResponse());
    }
}