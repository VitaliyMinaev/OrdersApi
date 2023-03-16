using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using CleanApi.Mappers;
using CleanApi.Repositories.Abstract;
using Domain;
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
        OrderDomain order = (await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)).ToDomain();

        order.MarkAsDelivered();
        
        var result = await _orderRepository.UpdateAsync(order.ToEntity(), cancellationToken);
        if (result == false)
            return Result.Fail(new Error("Can not update order", new Error("Database")));
        
        return Result.Ok(order.ToResponse());
    }
}