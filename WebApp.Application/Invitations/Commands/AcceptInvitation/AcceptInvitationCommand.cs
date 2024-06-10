
using MediatR;

namespace WebApp.Application.Invitations.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(Guid GatheringId, Guid InvitationId) : IRequest<Unit>;
