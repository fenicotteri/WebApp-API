
using Gatherly.Domain.Exceptions;
using WebApp.Domain.Errors;
using WebApp.Domain.Exceptions;
using WebApp.Domain.Primitives;
using WebApp.Domain.Shared;

namespace WebApp.Domain.Entities;

public sealed class Gathering : AggregateRoot
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();

    private Gathering(
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

    public Member Creator { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;

    public DateTime ScheduledAtUtc { get; private set; }

    public string? Location { get; private set; }

    public int MaximumNumberOfAttendees { get; private set; }

    public DateTime InvitationsExpireAtUtc { get; private set; }

    public int NumberOfAttendees { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees => _attendees;

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    public static Gathering Create(
        Guid id,
        Member creator,
        DateTime scheduledAtUtc,
        string name,
        string? location,
        int maximumNumberOfAttendees,
        int invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(
            id,
            creator,
            scheduledAtUtc,
            name,
            location);

        if (maximumNumberOfAttendees < 1)
        {
            throw new GatheringMaximumNumberOfAttendeesIsIncorrect(
                $"{nameof(maximumNumberOfAttendees)} can't be null.");
        }

        if (invitationsValidBeforeInHours < 0)
        {
            throw new GatheringInvitationsValidBeforeInHoursIsIncorrect(
                $"{nameof(invitationsValidBeforeInHours)} can't be null.");
        }

        gathering.InvitationsExpireAtUtc = gathering.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours);
        gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;

        return gathering;
    }
    public Result<Invitation> SendInvitation(Member member)
    {
        if (member.Id == Creator.Id)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);
        }

        if (ScheduledAtUtc <  DateTime.UtcNow)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);
        }

        Invitation invitation = new Invitation(new Guid(), member, this);

        _invitations.Add(invitation);

        return invitation;
    }

    public Result<Attendee> AcceptInvitation(Invitation invitation)
    {
        if (InvitationsExpireAtUtc <  DateTime.UtcNow || _attendees.Count == MaximumNumberOfAttendees)
        {
            invitation.Expire();

            return Result.Failure<Attendee>(DomainErrors.Gathering.Expired);
        }

        Attendee attendee = invitation.Accept();

        _attendees.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
}
