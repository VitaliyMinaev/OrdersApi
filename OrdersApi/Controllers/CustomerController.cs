using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Commands.CreateCustomerCommand;
using OrdersApi.Commands.DeleteCustomerCommand;
using OrdersApi.Commands.UpdateCustomerCommand;
using OrdersApi.Contracts;
using OrdersApi.Contracts.Requests;
using OrdersApi.Contracts.Responses.Customer;
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
        var models = await _mediator.Send(query, cancellationToken);
        return Ok(models.Select(x => new GetCustomerResponse { Id = x.Id, FullName = x.FullName, OrdersId = x.OrdersId}));
    }
    
    [HttpGet, Route(ApiRoutes.Customer.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);
        var model = await _mediator.Send(query, cancellationToken);
        return model == null ? NotFound() : Ok(new GetCustomerResponse { Id = model.Id, FullName = model.FullName, OrdersId = model.OrdersId});
    }

    [HttpPost, Route(ApiRoutes.Customer.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(request.FullName);
        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsFailed)
        {
            return CreateAndReturnFailedModelState(result.Errors);
        }

        var response = new CreatedCustomerResponse { Id = result.Value.Id, FullName = result.Value.FullName };
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Customer.GetById.Replace("{id}", response.Id.ToString())}";
        return Created(locationUrl, response);
    }

    [HttpPut, Route(ApiRoutes.Customer.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand(id, request.FullName);
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            return CreateAndReturnFailedModelState(result.Errors);
        }

        var response = new UpdatedCustomerResponse { Id = result.Value.Id, FullName = result.Value.FullName };
        return Ok(response);
    }

    [HttpDelete, Route(ApiRoutes.Customer.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailed == true ? CreateAndReturnFailedModelState(result.Errors) : NoContent();
    }

    private IActionResult CreateAndReturnFailedModelState(List<IError> errors)
    {
        var failedModel = _failedModelCreator.CreateErrorStateModel(errors);
        return ValidationProblem(failedModel);
    }
}