using CleanApi.Contracts.Responses;
using CleanApi.Mappers;
using CleanApi.Repositories.Abstract;
using MediatR;

namespace CleanApi.Queries.GetAllOrdersQuery;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return (await _orderRepository.GetAllAsync(cancellationToken)).Select(x => x.ToResponse());
    }
}