using FluentValidation;
using OrdersApi.Commands.UpdateCustomerCommand;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Validators;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(IRepository<CustomerEntity> customerRepository)
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .MustAsync(async (id, cancellation) => (await customerRepository.GetByIdAsync(id, cancellation)) != null)
            .WithMessage("There is no customer with given id");
    }
}