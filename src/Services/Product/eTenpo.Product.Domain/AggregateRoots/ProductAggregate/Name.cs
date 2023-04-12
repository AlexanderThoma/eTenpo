using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;
using eTenpo.Product.Domain.Exceptions.Base;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Name : ValueObject
{
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ProductValidationException("Name is null or whitespace",
                new ArgumentException(null, nameof(value)));
        }
        
        this.Value = value;
    }
    
    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value.ToLower();
    }
}