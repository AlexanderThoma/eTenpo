using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions.Base;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Description : ValueObject
{
    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new BadRequestException("Description is null or whitespace",
                new ArgumentException(null, nameof(value)));
        }
        
        if (value.Length > 255)
        {
            throw new BadRequestException("Description length must not exceed 255 characters");
        }
        
        this.Value = value;
    }
    
    public string Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value.ToLower();
    }
}