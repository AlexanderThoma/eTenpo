using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

public class Category : AggregateRoot
{
    public Category(Guid id) : base(id)
    {
    }
    
    
}