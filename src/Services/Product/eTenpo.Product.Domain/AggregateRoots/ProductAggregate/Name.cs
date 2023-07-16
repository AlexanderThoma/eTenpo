using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Name : ValueObject
{
    public const int MaxLength = 100;

    public Name(string value)
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value.ToLower();
    }
}