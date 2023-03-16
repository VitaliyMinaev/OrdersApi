using FluentResults;
using MediatR;

namespace CleanApi.Messaging.Abstract;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }