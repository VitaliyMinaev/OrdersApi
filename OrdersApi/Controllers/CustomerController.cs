using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Commands.CreateCustomerCommand;
using OrdersApi.Contracts;
using OrdersApi.Contracts.Requests;
using OrdersApi.Queries.GetCustomerByIdQuery;
using OrdersApi.Strategies.Abstract;

namespace OrdersApi.Controllers;

[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IModelStateCreator _failedModelCreator;

    public CustomerController(IMediator mediator, IModelStateCreator failedModelCreator)
    {
        _mediator = mediator;
        _failedModelCreator = failedModelCreator;
    }
    
    [HttpGet, Route(ApiRoutes.Customer.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);
        var response = await _mediator.Send(query, cancellationToken);
        return response == null ? NotFound() : Ok(response);
    }

    [HttpPost, Route(ApiRoutes.Customer.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(request.FullName);
        var response = await _mediator.Send(command, cancellationToken);
        if (response.IsFailed)
        {
            var failedModel = _failedModelCreator.CreateErrorStateModel(response.Errors);
            return ValidationProblem(failedModel);
        }

        return Created(ApiRoutes.Customer.GetById.Replace("{id}", response.Value.Id.ToString()), response.Value);
    }
}