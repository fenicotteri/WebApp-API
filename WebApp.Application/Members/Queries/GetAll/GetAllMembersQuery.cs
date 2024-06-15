using WebApp.Application.Abstractions.Messaging;

namespace WebApp.Application.Members.Queries.GetAll;

public sealed record GetAllMembersQuery : IQuery<AllMembersResponse>;

