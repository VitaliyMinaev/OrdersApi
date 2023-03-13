using CleanApi.Contracts;
using CleanApi.Contracts.Responses;
using CleanApi.Queries.GetAllOrdersQuery;
using CleanApi.Queries.GetOrderByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanApi.Controllers;

public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
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
}