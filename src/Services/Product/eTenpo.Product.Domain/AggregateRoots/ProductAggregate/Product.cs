using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Product : AggregateRoot
{
    public Product(Guid id) : base(id)
    {
    }
    
    
}