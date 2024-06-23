
using WebApp.Domain.Errors;
using WebApp.Domain.Primitives;
using WebApp.Domain.Shared;

namespace WebApp.Domain.ValueObjects;
public sealed class FirstName : ValueObject
{
    public const int MaxLength = 100;
    public string Value { get; private set; }
    private FirstName(string value) => Value = value;
    private FirstName()
    {
    }

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<FirstName>(DomainErrors.FirstName.Empty);
        }

        if (value.Length > MaxLength)
        {
            return Result.Failure<FirstName>(DomainErrors.FirstName.TooLong);
        }

        return new FirstName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
