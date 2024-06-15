
using MediatR;
using WebApp.Application.Abstractions.Messaging;
using WebApp.Domain.Shared;

namespace WebApp.Application.Members.Queries.GetById;

public sealed record GetMemberByIdQuery(Guid MemberId) : IQuery<MemberResponse>;
