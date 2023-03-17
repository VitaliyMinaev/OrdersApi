using CleanApi.Commands.CreateCustomerCommand;
using CleanApi.Entities;
using CleanApi.Repositories.Abstract;
using FluentValidation;

namespace CleanApi.Validators;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(IRepository<CustomerEntity> customerRepository)
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MustAsync(async (fullName, cancellation) =>
            {
                var customers = await customerRepository.GetAllAsync(cancellation);
                return customers.Any(x => x.FullName == fullName) == false;
            })
            .WithMessage("Customer with same fullname already exist!");
    }
}