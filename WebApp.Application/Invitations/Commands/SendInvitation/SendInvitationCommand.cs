

using MediatR;
using WebApp.Domain.Shared;

namespace WebApp.Application.Invitations.Commands.SendInvitation;

public sealed record SendInvitationCommand(Guid MemberId, Guid GatheringId) : IRequest<Result>;
