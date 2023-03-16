using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using CleanApi.Mappers;
using CleanApi.Repositories.Abstract;
using FluentResults;
using MediatR;

namespace CleanApi.Commands.UpdateOrderCommand;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result<OrderResponse>>
{
    private readonly IRepository<OrderEntity> _orderRepository;
    public UpdateOrderHandler(IRepository<OrderEntity> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderEntity order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        
        order.Delivered = request.Delivered;
        
        var result = await _orderRepository.UpdateAsync(order, cancellationToken);
        if (result == false)
            return Result.Fail(new Error("Can not update order", new Error("Database")));
        
        return Result.Ok(order.ToResponse());
    }
}