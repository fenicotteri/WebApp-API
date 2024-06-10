using WebApp.Domain.Exceptions;

namespace Gatherly.Domain.Exceptions;

public sealed class GatheringMaximumNumberOfAttendeesIsIncorrect : DomainException
{
    public GatheringMaximumNumberOfAttendeesIsIncorrect(string message) : base(message)
    {
    }
}