using FluentResults;
using MediatR;
using OrdersApi.Entities;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.DeleteOrderCommand;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Result>
{
    private readonly IRepository<OrderEntity> _orderRepository;
    public DeleteOrderHandler(IRepository<OrderEntity> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.DeleteAsync(request.OrderId, cancellationToken);
        
        if(result == false)
            return Result.Fail(new Error("Can not delete order", new Error("Internal server error")));
        return Result.Ok();
    }
}