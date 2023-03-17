using FluentValidation;
using OrdersApi.Commands.DeleteCustomerCommand;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Validators;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator(IRepository<CustomerEntity> customerRepository)
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .MustAsync(async (id, cancellation) =>
            {
                var exists = await customerRepository.GetByIdAsync(id, cancellation);
                return exists != null;
            })
            .WithMessage("There is no customer with given id");
    }
}