using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

public class Category : AggregateRoot, IAuditable
{
    public Category(CategoryName name, CategoryDescription description)
    {
        this.Name = name;
        this.Description = description;
        this.Products = new List<Guid>();
    }
    
    public CategoryName Name { get; init; }
    public CategoryDescription Description { get; init; }
    public List<Guid>? Products { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
}