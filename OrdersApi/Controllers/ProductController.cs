using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Commands.CreateProductCommand;
using OrdersApi.Commands.DeleteProductCommand;
using OrdersApi.Commands.UpdateProductCommand;
using OrdersApi.Contracts;
using OrdersApi.Contracts.Requests;
using OrdersApi.Contracts.Responses.Product;
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
        var models = await _mediator.Send(query, cancellationToken);
        return Ok(models.Select(x => new GetProductResponse { Id = x.Id, Name = x.Name, Price = x.Price, ReleaseDate = x.ReleaseDate}));
    }
    
    [HttpGet, Route(ApiRoutes.Product.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var model = await _mediator.Send(query, cancellationToken);
        return model == null ? NotFound() : Ok(new GetProductResponse { Id = model.Id, Name = model.Name, Price = model.Price, ReleaseDate = model.ReleaseDate});
    }
    
    [HttpPost, Route(ApiRoutes.Product.Create)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(request.Name, request.Price, request.ReleaseDate);
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
            return CreateAndReturnFailedModelState(result.Errors);

        var response = new CreatedProductResponse { Id = result.Value.Id, Name = result.Value.Name, Price = result.Value.Price, ReleaseDate = result.Value.ReleaseDate };
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Product.GetById.Replace("{id}", response.Id.ToString())}";
        return Created(locationUrl, response);
    }
    
    [HttpPut, Route(ApiRoutes.Product.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(id, request.Name, request.Price, request.ReleaseDate);
        var result = await _mediator.Send(command, cancellationToken);
        
        if (result.IsFailed)
            return CreateAndReturnFailedModelState(result.Errors);
        
        var response = new UpdatedProductResponse { Id = result.Value.Id, Name = result.Value.Name, Price = result.Value.Price, ReleaseDate = result.Value.ReleaseDate };
        return Ok(response);
    }

    [HttpDelete, Route(ApiRoutes.Product.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailed == true ? CreateAndReturnFailedModelState(result.Errors) : NoContent();
    }

    private IActionResult CreateAndReturnFailedModelState(List<IError> errors)
    {
        var failedModel = _failedModelCreator.CreateErrorStateModel(errors);
        return ValidationProblem(failedModel);
    }
}
