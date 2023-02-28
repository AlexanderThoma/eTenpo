using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Product : AggregateRoot
{
    public Product(Guid id) : base(id)
    {
    }
    
    public string Description { get; set; }
    
    public string Name { get; set; }

    public string[] Tags { get; set; }
    
    public decimal Price { get; set; }
    
    public Guid[] Categories { get; set; }
}