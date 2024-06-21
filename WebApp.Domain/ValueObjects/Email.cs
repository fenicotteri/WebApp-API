
using WebApp.Domain.Errors;
using WebApp.Domain.Primitives;
using WebApp.Domain.Shared;

namespace WebApp.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private Email(string value) => Value = value;

    public static explicit operator string(Email email) => email.Value;

    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<Email>(DomainErrors.Email.Empty);
        }

        if (email.Split('@').Length != 2)
        {
            return Result.Failure<Email>(DomainErrors.Email.InvalidFormat);
        }

        return new Email(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

