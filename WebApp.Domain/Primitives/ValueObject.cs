
namespace WebApp.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEquel(other);
    }

    private bool ValuesAreEquel(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(
            default(int),
            HashCode.Combine);
    }

    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEquel(other);
    }
}
