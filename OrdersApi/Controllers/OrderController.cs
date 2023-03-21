using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Commands.CreateOrderCommand;
using OrdersApi.Commands.DeleteOrderCommand;
using OrdersApi.Commands.UpdateOrderCommand;
using OrdersApi.Contracts;
using OrdersApi.Contracts.Requests;
using OrdersApi.Contracts.Responses.Order;
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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllOrdersQuery();
        var models = await _mediator.Send(query, cancellationToken);
        return Ok(models.Select(x => new GetOrderResponse { Id = x.Id, Customer = x.Customer, Product = x.Product, Delivered = x.Delivered, DeliveryDate = x.DeliveryDate}));
    }

    [HttpGet, Route(ApiRoutes.Order.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(id);
        var model = await _mediator.Send(query, cancellationToken);
        return model == null ? NotFound() : Ok(new GetOrderResponse { Id = model.Id, Customer = model.Customer, Product = model.Product, Delivered = model.Delivered, DeliveryDate = model.DeliveryDate});
    }

    [HttpPost, Route(ApiRoutes.Order.Create)]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(request.ProductId, request.CustomerId);
        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailed)
        {
            return CreateAndReturnFailedModelState(result.Errors);
        }
        
        var response = new CreatedOrderResponse { Id = result.Value.Id, Customer = result.Value.Customer, Product = result.Value.Product, Delivered = result.Value.Delivered, DeliveryDate = result.Value.DeliveryDate};
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Order.GetById.Replace("{id}", response.Id.ToString())}";
        return Created(locationUrl, response);
    }

    [HttpPut, Route(ApiRoutes.Order.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateOrderCommand(id, request.Delivered);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailed == true ? CreateAndReturnFailedModelState(result.Errors) : Ok(
            new UpdatedOrderResponse { Id = result.Value.Id, Customer = result.Value.Customer, Product = result.Value.Product, Delivered = result.Value.Delivered, DeliveryDate = result.Value.DeliveryDate});
    }

    [HttpDelete, Route(ApiRoutes.Order.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
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