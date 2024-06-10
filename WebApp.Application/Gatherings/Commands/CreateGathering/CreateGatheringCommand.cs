
using MediatR;
using WebApp.Domain.Shared;

namespace WebApp.Application.Gatherings.Commands.CreateGathering;

public sealed record CreateGatheringCommand(
        Guid CreatorId,
        DateTime scheduledAtUtc,
        string Name,
        string? Location,
        int MaximumNumberOfAttendees,
        int InvitationsValidBeforeInHours) : IRequest<Result<Guid>>;

