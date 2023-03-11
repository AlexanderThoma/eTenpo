using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class CategoryId : ValueObject
{
    public CategoryId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ProductDomainException("CategoryId is an empty guid",
                new ArgumentException(null, nameof(value)));
        }
        
        this.Value = value;
    }
    
    public Guid Value { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}