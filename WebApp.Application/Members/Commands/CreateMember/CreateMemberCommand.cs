using MediatR;

namespace WebApp.Application.Members.Commands.CreateMember;
public sealed record CreateMemberCommand(
    string Email,
    string FirstName,
    string LastName) : IRequest<Guid>;
