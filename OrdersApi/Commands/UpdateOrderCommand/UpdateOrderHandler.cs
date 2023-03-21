using OrdersApi.Mappers;
using Domain;
using FluentResults;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Models;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.UpdateOrderCommand;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result<OrderModel>>
{
    private readonly IRepository<OrderEntity> _orderRepository;
    public UpdateOrderHandler(IRepository<OrderEntity> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderModel>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderDomain order = (await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)).ToDomain();

        order.ChangeDeliveryStatus(request.Delivered);
        
        var result = await _orderRepository.UpdateAsync(order.ToEntity(), cancellationToken);
        if (result == false)
            return Result.Fail(new Error("Can not update order", new Error("Database")));
        
        return Result.Ok(order.ToModel());
    }
}