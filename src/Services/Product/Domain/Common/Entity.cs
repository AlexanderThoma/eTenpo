namespace eTenpo.Product.Domain.Common;

/// <summary>
/// Mutable object with an identity (guid)
/// </summary>
public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Entity objEntity)
        {
            return false;
        }

        if (ReferenceEquals(this, objEntity))
        {
            return true;
        }

        return this.Id == objEntity.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public static bool operator ==(Entity left, Entity right)
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

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}