
using WebApp.Domain.Shared;

namespace WebApp.Domain.Errors;

public static class DomainErrors
{
    public static class Member
    {
        public static readonly Error EmailAlreadyInUse = new(
            "Member.EmailAlreadyInUse",
            "The specified email is already in use.");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Member.NotFound",
            $"The member with the identifier {id} was not found.");
    }

    public static class Gathering
    {
        public static readonly Error InvitingCreator = new(
            "Invitation.InvitingCreator",
            "You can`t invite the creator of gathering.");

        public static readonly Error AlreadyPassed = new(
            "Invitation.AlreadyPassed",
            "Can't send invitation for gathering in the past.");
    }
}
