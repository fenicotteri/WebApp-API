

using MediatR;

namespace WebApp.Application.Invitations.Commands.SendInvitation;

public sealed record SendInvitationCommand(Guid MemberId, Guid GatheringId) : IRequest<Unit>;
