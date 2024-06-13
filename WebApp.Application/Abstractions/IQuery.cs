using MediatR;
using WebApp.Domain.Shared;

namespace WebApp.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}