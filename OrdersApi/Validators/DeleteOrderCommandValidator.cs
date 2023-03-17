using FluentValidation;
using OrdersApi.Commands.DeleteOrderCommand;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Validators;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator(IRepository<OrderEntity> orderRepository)
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .MustAsync(async (id, cancellation) =>
            {
                var exists = await orderRepository.GetByIdAsync(id, cancellation);
                return exists != null;
            })
            .WithMessage("There is no order with given id");
    }
}