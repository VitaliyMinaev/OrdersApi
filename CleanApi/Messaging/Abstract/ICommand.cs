using FluentResults;
using MediatR;

namespace CleanApi.Messaging.Abstract;

public interface ICommand : IRequest<Result>
{ }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{ }