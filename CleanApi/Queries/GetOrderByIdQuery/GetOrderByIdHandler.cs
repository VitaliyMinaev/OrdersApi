using CleanApi.Contracts.Responses;
using CleanApi.Mappers;
using CleanApi.Repositories.Abstract;
using MediatR;

namespace CleanApi.Queries.GetOrderByIdQuery;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponse?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return (await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken))?.ToResponse();
    }
}