

using WebApp.Domain.Entities;

namespace WebApp.Presentation.Contracts.Gatherings;

 public sealed record RegisterGatheringRequest(
        Guid CreatorId,
        DateTime scheduledAtUtc,
        string Name,
        string? Location,
        int MaximumNumberOfAttendees,
        int InvitationsValidBeforeInHours);
