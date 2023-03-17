using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Commands.CreateOrderCommand;
using OrdersApi.Commands.DeleteOrderCommand;
using OrdersApi.Commands.UpdateOrderCommand;
using OrdersApi.Contracts;
using OrdersApi.Contracts.Requests;
using OrdersApi.Contracts.Responses;
using OrdersApi.Queries.GetAllOrdersQuery;
using OrdersApi.Queries.GetOrderByIdQuery;
using OrdersApi.Strategies.Abstract;

namespace OrdersApi.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IModelStateCreator _failedModelCreator;
    public OrderController(IMediator mediator, IModelStateCreator failedModelCreator)
    {
        _mediator = mediator;
        _failedModelCreator = failedModelCreator;
    }

    [HttpGet, Route(ApiRoutes.Order.GetAll)]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        var query = new GetAllOrdersQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet, Route(ApiRoutes.Order.GetById)]
    public async Task<IActionResult> GetOrder([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(id);
        OrderResponse? response = await _mediator.Send(query, cancellationToken);
        return response == null ? NotFound() : Ok(response);
    }

    [HttpPost, Route(ApiRoutes.Order.Create)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(request.ProductId, request.CustomerId);
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailed)
        {
            return CreateAndReturnFailedModelState(response.Errors);
        }
        
        var created = response.Value;
        return Created(ApiRoutes.Order.GetById.Replace("{id}", created.Id.ToString()), created);
    }

    [HttpPut, Route(ApiRoutes.Order.Update)]
    public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateOrderCommand(id, request.Delivered);
        Result<OrderResponse> response = await _mediator.Send(command, cancellationToken);
        if (response.IsFailed)
        {
            return CreateAndReturnFailedModelState(response.Errors);
        }
        return Ok(response.Value);
    }

    [HttpDelete, Route(ApiRoutes.Order.Delete)]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrderCommand(id);
        Result response = await _mediator.Send(command, cancellationToken);
        return response.IsFailed == true ? CreateAndReturnFailedModelState(response.Errors) : NoContent();
    }
    
    private IActionResult CreateAndReturnFailedModelState(List<IError> errors)
    {
        var failedModel = _failedModelCreator.CreateErrorStateModel(errors);
        return ValidationProblem(failedModel);
    }
}