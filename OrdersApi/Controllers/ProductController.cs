using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Commands.CreateProductCommand;
using OrdersApi.Commands.DeleteProductCommand;
using OrdersApi.Commands.UpdateProductCommand;
using OrdersApi.Contracts;
using OrdersApi.Contracts.Requests;
using OrdersApi.Queries.GetAllProductsQuery;
using OrdersApi.Queries.GetProductByIdQuery;
using OrdersApi.Strategies.Abstract;

namespace OrdersApi.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IModelStateCreator _failedModelCreator;
    public ProductController(IMediator mediator, IModelStateCreator failedModelCreator)
    {
        _mediator = mediator;
        _failedModelCreator = failedModelCreator;
    }

    [HttpGet, Route(ApiRoutes.Product.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllProductsQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
    [HttpGet, Route(ApiRoutes.Product.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var response = await _mediator.Send(query, cancellationToken);
        return response == null ? NotFound() : Ok(response);
    }
    [HttpPost, Route(ApiRoutes.Product.Create)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(request.Name, request.Price, request.ReleaseDate);
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailed)
            return CreateAndReturnFailedModelState(response.Errors);

        return Created(ApiRoutes.Product.GetById.Replace("{id}", response.Value.Id.ToString()), response.Value);
    }
    [HttpPut, Route(ApiRoutes.Product.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(id, request.Name, request.Price, request.ReleaseDate);
        var response = await _mediator.Send(command, cancellationToken);
        
        if (response.IsFailed)
            return CreateAndReturnFailedModelState(response.Errors);

        return Ok(response.Value);
    }

    [HttpDelete, Route(ApiRoutes.Product.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        var response = await _mediator.Send(command, cancellationToken);
        
        if (response.IsFailed)
            return CreateAndReturnFailedModelState(response.Errors);

        return NoContent();
    }

    private IActionResult CreateAndReturnFailedModelState(List<IError> errors)
    {
        var failedModel = _failedModelCreator.CreateErrorStateModel(errors);
        return ValidationProblem(failedModel);
    }
}
