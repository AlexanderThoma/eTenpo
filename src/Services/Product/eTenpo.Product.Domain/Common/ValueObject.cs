namespace eTenpo.Product.Domain.Common;

/// <summary>
/// Simple immutable object without an identity (Id property)
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Contains a list of all properties which are used for equality comparison
    /// </summary>
    /// <returns>List of all equality relevant properties</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        // null-check, type comparison
        if (obj is not ValueObject inputObject)
        {
            return false;
        }

        return GetEqualityComponents().SequenceEqual(inputObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
        {
            return true;
        }

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }
}

