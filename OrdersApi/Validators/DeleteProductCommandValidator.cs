using FluentValidation;
using OrdersApi.Commands.DeleteProductCommand;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator(IRepository<ProductEntity> productRepository)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (productId, cancellation) =>
                (await productRepository.GetByIdAsync(productId, cancellation)) != null)
            .WithMessage("There is no product with given id");
    }
}