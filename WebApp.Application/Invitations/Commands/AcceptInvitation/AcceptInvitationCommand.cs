
using MediatR;
using WebApp.Domain.Shared;

namespace WebApp.Application.Invitations.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(Guid GatheringId, Guid InvitationId) : IRequest<Result>;
