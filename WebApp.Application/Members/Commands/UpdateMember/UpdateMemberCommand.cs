using WebApp.Application.Abstractions.Messaging;

namespace WebApp.Application.Members.Commands.UpdateMember;

public sealed record UpdateMemberCommand(Guid MemberId, string FirstName, string LastName) : ICommand;
