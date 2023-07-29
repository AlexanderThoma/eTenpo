using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

public class Category : AggregateRoot, IAuditable
{
    public Category(Name name, Description description)
    {
        this.Name = name;
        this.Description = description;
        this.Products = new List<Guid>();
    }
    
    public Name Name { get; init; }
    public Description Description { get; init; }
    public List<Guid> Products { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
}