using OrdersApi.Mappers;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Models;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Queries.GetOrderByIdQuery;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderModel?>
{
    private readonly IRepository<OrderEntity> _orderRepository;
    public GetOrderByIdHandler(IRepository<OrderEntity> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderModel?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        return order?.ToModel();
    }
}