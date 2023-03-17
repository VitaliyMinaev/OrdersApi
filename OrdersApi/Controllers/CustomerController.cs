using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Commands.CreateCustomerCommand;
using OrdersApi.Commands.DeleteCustomerCommand;
using OrdersApi.Commands.UpdateCustomerCommand;
using OrdersApi.Contracts;
using OrdersApi.Contracts.Requests;
using OrdersApi.Queries.GetAllCustomersQuery;
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

    [HttpGet, Route(ApiRoutes.Customer.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCustomersQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
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
            return CreateAndReturnFailedModelState(response.Errors);
        }

        return Created(ApiRoutes.Customer.GetById.Replace("{id}", response.Value.Id.ToString()), response.Value);
    }

    [HttpPut, Route(ApiRoutes.Customer.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand(id, request.FullName);
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailed)
        {
            return CreateAndReturnFailedModelState(response.Errors);
        }

        return Ok(response.Value);
    }

    [HttpDelete, Route(ApiRoutes.Customer.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerCommand(id);
        var response = await _mediator.Send(command, cancellationToken);

        return response.IsFailed == true ? CreateAndReturnFailedModelState(response.Errors) : NoContent();
    }

    private IActionResult CreateAndReturnFailedModelState(List<IError> errors)
    {
        var failedModel = _failedModelCreator.CreateErrorStateModel(errors);
        return ValidationProblem(failedModel);
    }
}