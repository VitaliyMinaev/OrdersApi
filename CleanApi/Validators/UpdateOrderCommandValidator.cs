using CleanApi.Commands.UpdateOrderCommand;
using CleanApi.Entities;
using CleanApi.Repositories.Abstract;
using FluentValidation;

namespace CleanApi.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator(IRepository<OrderEntity> orderRepository)
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .MustAsync(async (id, cancellation) => (await orderRepository.GetByIdAsync(id, cancellation)) != null)
            .WithMessage("There is no product with given id");
    }
}