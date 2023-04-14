using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;
using eTenpo.Product.Domain.Exceptions.Base;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Price : ValueObject
{
    public Price(decimal value)
    {
        if (value <= 0)
        {
            throw new ProductValidationException("Price can not be zero or negative",
                new ArgumentException(null, nameof(value)));
        }

        this.Value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    public decimal Value { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        // round for comparison as only two digits are relevant
        yield return Math.Round(this.Value, 2, MidpointRounding.AwayFromZero);
    }
}