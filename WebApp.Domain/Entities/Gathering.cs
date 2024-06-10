
using WebApp.Domain.Errors;
using WebApp.Domain.Primitives;
using WebApp.Domain.Shared;

namespace WebApp.Domain.Entities;

public sealed class Gathering : AggregateRoot
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

    public Member Creator { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;

    public DateTime ScheduledAtUtc { get; private set; }

    public string? Location { get; private set; }

    public int? MaximumNumberOfAttendees { get; private set; }

    public DateTime? InvitationsExpireAtUtc { get; private set; }

    public int NumberOfAttendees { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees => _attendees;

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

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

}
