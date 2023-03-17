using OrdersApi.Entities.Abstract;
using FluentValidation;
using OrdersApi.Commands.CreateOrderCommand;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(IRepository<CustomerEntity> customerRepository, IRepository<ProductEntity> productRepository)
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .MustAsync(async (id, cancellation) => await ExistInCustomerRepositoryAsync(id, customerRepository, cancellation))
            .WithMessage("There is no product with given id");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (id, cancellation) => await ExistInProductRepositoryAsync(id, productRepository, cancellation))
            .WithMessage("There is no product with given id");
    }
    
    async Task<bool> ExistInCustomerRepositoryAsync(Guid id, IRepository<CustomerEntity> repository, CancellationToken cancellation)
    {
        var exists = await repository.GetByIdAsync(id, cancellation);
        return exists != null;
    }
    async Task<bool> ExistInProductRepositoryAsync(Guid id, IRepository<ProductEntity> repository, CancellationToken cancellation)
    {
        var exists = await repository.GetByIdAsync(id, cancellation);
        return exists != null;
    }
}