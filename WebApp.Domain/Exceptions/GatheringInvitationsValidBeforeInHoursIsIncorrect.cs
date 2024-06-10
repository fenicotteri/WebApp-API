namespace WebApp.Domain.Exceptions;

public sealed class GatheringInvitationsValidBeforeInHoursIsIncorrect : DomainException
{
    public GatheringInvitationsValidBeforeInHoursIsIncorrect(string message) : base(message)
    {
    }
}