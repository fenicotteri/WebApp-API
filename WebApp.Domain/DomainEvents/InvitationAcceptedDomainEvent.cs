
using WebApp.Domain.Primitives;

namespace WebApp.Domain.DomainEvents;

public sealed record InvitationAcceptedDomainEvent(Guid InvitationId, Guid GatheringId) : IDomainEvent;
