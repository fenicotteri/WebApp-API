using WebApp.Domain.Primitives;
using WebApp.Domain.ValueObjects;

namespace WebApp.Domain.Entities;

public sealed class Member : AggregateRoot
{
    private Member(Guid id, Email email, FirstName firstName, LastName lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        CreatedOnUtc = DateTime.UtcNow;
    }

    private Member()
    {
    }

    public Email Email { get; private set; } = null!;

    public FirstName FirstName { get; private set; } = null!;

    public LastName LastName { get; private set; } = null!;

    public DateTime CreatedOnUtc { get; init; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public static Member Create(
        Guid id,
        Email email,
        FirstName firstName,
        LastName lastName)
    {
        var member = new Member(
            id,
            email,
            firstName,
            lastName);

        return member;
    }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }


}

