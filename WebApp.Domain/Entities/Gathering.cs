﻿using Domain.Primitives;
using WebApp.Domain.Entities;

namespace WebApp.Domain.Entities;

public sealed class Gathering : Entity
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();

    public Gathering(
        Guid id,
        Member creator,
        DateTime scheduledAtUtc,
        string name,
        string? location)
        : base(id)
    {
        Creator = creator;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
    }

    private Gathering()
    {
    }

    public Member Creator { get; private set; }

    public string Name { get; private set; }

    public DateTime ScheduledAtUtc { get; private set; }

    public string? Location { get; private set; }

    public int? MaximumNumberOfAttendees { get; private set; }

    public DateTime? InvitationsExpireAtUtc { get; private set; }

    public int NumberOfAttendees { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees => _attendees;

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

}