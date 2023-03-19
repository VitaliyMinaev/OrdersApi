using FluentValidation;
using OrdersApi.Commands.UpdateOrderCommand;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator(IRepository<OrderEntity> orderRepository)
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .MustAsync(async (id, cancellation) => (await orderRepository.GetByIdAsync(id, cancellation)) != null)
            .WithMessage("There is no order with given id");
    }
}