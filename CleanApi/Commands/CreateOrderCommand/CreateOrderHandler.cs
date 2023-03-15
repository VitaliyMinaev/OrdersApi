using CleanApi.Contracts.Responses;
using CleanApi.Entities;
using CleanApi.Mappers;
using CleanApi.Repositories.Abstract;
using MediatR;

namespace CleanApi.Commands.CreateOrderCommand;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
{
    private readonly IRepository<OrderEntity> _orderRepository;

    public CreateOrderHandler(IRepository<OrderEntity> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);
        if (orders is null || orders.Count() == 0)
        {
            throw new NotImplementedException();
        }

        var firstOrder = orders.First();
        var orderEntity = new OrderEntity
        {
            Id = Guid.NewGuid(),
            Customer = firstOrder.Customer,
            Product = firstOrder.Product,
            Delivered = false,
            DeliveryDate = DateTime.Now.AddDays(Random.Shared.Next(2, 7))
        };
        
        var result = await _orderRepository.AddAsync(orderEntity, cancellationToken);

        if (result == false)
            throw new InvalidOperationException();

        return orderEntity.ToResponse();
    }
}