using MediatR;
using WebApp.Domain.Shared;

namespace WebApp.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}