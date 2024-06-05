
using MediatR;

namespace WebApp.Application.Members.Queries.GetById;

public sealed record GetMemberByIdQuery(Guid MemberId) : IRequest<MemberResponse>;
