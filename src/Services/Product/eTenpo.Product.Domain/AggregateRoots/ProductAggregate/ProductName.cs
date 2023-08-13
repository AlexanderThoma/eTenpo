using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class ProductName : ValueObject
{
    public const int MaxLength = 100;

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ProductValidationException("Name is null or whitespace",
                new ArgumentException(null, nameof(value)));
        }

        if (value.Length > MaxLength)
        {
            throw new ProductValidationException($"Description length must not exceed {MaxLength} characters");
        }

        this.Value = value;
    }

    public string Value { get; init; }

    public static implicit operator string(ProductName name)
    {
        return name.Value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value.ToLower();
    }
}