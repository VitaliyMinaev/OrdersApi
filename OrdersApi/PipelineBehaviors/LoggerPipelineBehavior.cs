using FluentResults;
using MediatR;

namespace OrdersApi.PipelineBehaviors;

public class LoggerPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggerPipelineBehavior<TRequest, TResponse>> _logger;
    public LoggerPipelineBehavior(ILogger<LoggerPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request: {request.ToString()}");
        return await next();
    }
}