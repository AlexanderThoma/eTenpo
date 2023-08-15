namespace eTenpo.Product.Domain.Common;

public interface IAuditable
{
    DateTime CreatedOnUtc { get; }

    DateTime? ModifiedOnUtc { get; }
}