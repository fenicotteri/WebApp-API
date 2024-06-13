
using WebApp.Domain.Entities;
using WebApp.Domain.Primitives;

namespace WebApp.Domain.DomainEvents;

public sealed record InvitationAcceptedDomainEvent(Attendee Attendee) : IDomainEvent;
