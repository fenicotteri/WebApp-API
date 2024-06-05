using WebApp.Domain.Primitieves;

namespace WebApp.Domain.Entities;

public sealed class Member : AuditableEntity
{
    public Member(Guid id, string email, string firstName, string lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

}

