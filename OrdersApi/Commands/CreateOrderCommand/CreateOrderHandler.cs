using OrdersApi.Mappers;
using FluentResults;
using MediatR;
using OrdersApi.Contracts.Responses;
using OrdersApi.Entities;
using OrdersApi.Models;
using OrdersApi.Repositories.Abstract;

namespace OrdersApi.Commands.CreateOrderCommand;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result<OrderModel>>
{
    private readonly IRepository<OrderEntity> _orderRepository;
    private readonly IRepository<CustomerEntity> _customerRepository;
    private readonly IRepository<ProductEntity> _productRepository;
    
    public CreateOrderHandler(IRepository<OrderEntity> orderRepository, IRepository<CustomerEntity> customerRepository, IRepository<ProductEntity> productRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<OrderModel>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = (await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken)).ToDomain();
        var product = (await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)).ToDomain();

        // Business logic
        var order = customer.OrderProduct(product);
        
        var result = await _orderRepository.AddAsync(order.ToEntity(), cancellationToken);

        if (result == false)
            throw new InvalidOperationException();

        return Result.Ok(order.ToModel());
    }
}