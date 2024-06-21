using WebApp.Application.Abstractions.Messaging;
using WebApp.Domain;

namespace WebApp.Application.Members.Queries.GetAll;

public sealed record GetAllMembersQuery(MemberQueryObjectRequest QueryObjectRequest) : IQuery<AllMembersResponse>;

