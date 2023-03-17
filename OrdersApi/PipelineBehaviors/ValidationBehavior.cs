using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace OrdersApi.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ResultBase<TResponse>, new()
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationFailures = new List<ValidationFailure>();
        foreach (var validator in _validators)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            foreach (var failure in validationResult.Errors)
            {
                if(failure != null)
                    validationFailures.Add(failure);
            }
        }
        
        if (validationFailures.Any())
        {
            _logger.LogWarning("There are some validation failures");
            
            var responseType = typeof(TResponse);
            TResponse invalidResponse;
            if (responseType.IsGenericType)
            {
                var resultType = responseType.GetGenericArguments()[0];
                var invalidResponseType = typeof(Result<>).MakeGenericType(resultType);

                invalidResponse = Activator.CreateInstance(invalidResponseType, null) as TResponse;
            }
            else
            {
                invalidResponse = new TResponse();
            }

            invalidResponse.WithErrors(validationFailures.Select(x => new Error(x.ErrorMessage, 
                new Error(x.PropertyName))));
            return invalidResponse;
        }

        return await next();
    }
}