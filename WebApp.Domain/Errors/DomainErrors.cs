﻿
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

        public static readonly Error Expired = new(
            "Gathering.Expired",
            "Can't accept invitation for expired gathering");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Gathering.NotFound",
            $"The gathering with the identifier {id} was not found.");
    }

    public static class Invitation
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Invitation.NotFound",
            $"The invitation with the identifier {id} was not found.");
    }

    public static class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");
    }

    public static class FirstName
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "FirstName name is too long");
    }

    public static class LastName
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name is empty");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "Last name is too long");
    }
}
