using FluentValidation;
using OrdersApi.Commands.UpdateProductCommand;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IRepository<ProductEntity> productRepository)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (productId, cancellation) =>
                (await productRepository.GetByIdAsync(productId, cancellation)) != null)
            .WithMessage("There is no product with given id");
    }
}