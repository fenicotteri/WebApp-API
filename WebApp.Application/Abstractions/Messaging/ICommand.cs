using MediatR;
using WebApp.Domain.Shared;

namespace WebApp.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
