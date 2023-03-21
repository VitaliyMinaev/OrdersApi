using OrdersApi.Mappers;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Models;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Queries.GetAllOrdersQuery;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderModel>>
{
    private readonly IRepository<OrderEntity> _orderRepository;
    public GetAllOrdersHandler(IRepository<OrderEntity> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);
        return (orders.Select(x => x.ToModel()));
    }
}