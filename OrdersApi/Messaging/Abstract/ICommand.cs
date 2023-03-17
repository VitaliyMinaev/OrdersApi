using FluentResults;
using MediatR;

namespace OrdersApi.Messaging.Abstract;

public interface ICommand : IRequest<Result>
{ }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{ }