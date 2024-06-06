
using MediatR;
using WebApp.Domain.Shared;

namespace WebApp.Application.Members.Queries.GetById;

public sealed record GetMemberByIdQuery(Guid MemberId) : IRequest<Result<MemberResponse>>;
