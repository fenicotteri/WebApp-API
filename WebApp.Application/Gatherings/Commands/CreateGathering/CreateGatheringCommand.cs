using WebApp.Application.Abstractions.Messaging;

namespace WebApp.Application.Gatherings.Commands.CreateGathering;

public sealed record CreateGatheringCommand(
        Guid CreatorId,
        DateTime scheduledAtUtc,
        string Name,
        string? Location,
        int MaximumNumberOfAttendees,
        int InvitationsValidBeforeInHours) : ICommand<Guid>;

