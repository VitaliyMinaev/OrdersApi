using FluentResults;
using MediatR;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.DeleteCustomerCommand;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Result>
{
    private readonly IRepository<CustomerEntity> _customerEntity;
    public DeleteCustomerHandler(IRepository<CustomerEntity> customerEntity)
    {
        _customerEntity = customerEntity;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _customerEntity.DeleteAsync(request.CustomerId, cancellationToken);
        if(result == false)
            return Result.Fail(new Error("Can not delete customer", new Error("Internal server error")));
        
        return Result.Ok();
    }
}