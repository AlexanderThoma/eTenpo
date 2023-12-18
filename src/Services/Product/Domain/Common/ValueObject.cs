namespace eTenpo.Product.Domain.Common;

/// <summary>
/// Simple immutable object with an identity defined by its properties,
/// changing it's properties changes its identity
/// has to have checks inside ctor for checking its own valid state
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Contains a list of all properties which are used for equality comparison
    /// Must be added by "yield return" to get one element at a time
    /// </summary>
    /// <returns>List of all equality relevant properties</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// checks equality based on all existing properties
    /// </summary>
    /// <param name="obj">object to be compared to</param>
    /// <returns></returns>
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

