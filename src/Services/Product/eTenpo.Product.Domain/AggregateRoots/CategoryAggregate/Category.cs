using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

public class Category : AggregateRoot, IAuditable
{
    private List<ProductAggregate.Product> products = new ();

    // used by EF core migration
#pragma warning disable CS8618
    private Category(){}
#pragma warning restore CS8618
    
    public Category(string name, string description)
    {
        this.Name = new CategoryName(name);
        this.Description = new CategoryDescription(description);
    }
    
    public CategoryName Name { get; private set; }
    
    public CategoryDescription Description { get; private set; }

    public IReadOnlyCollection<ProductAggregate.Product> Products => products.AsReadOnly();

    public DateTime CreatedOnUtc { get; }
    
    public DateTime? ModifiedOnUtc { get; }
    
    public void Delete()
    {
        if (this.products.Any())
        {
            throw new CategoryValidationException(
                "There are still products using this category. Delete them or assign a different category");
        }
    }

    public void UpdateName(CategoryName name)
    {
        if (this.Name == name)
        {
            return; 
        }

        this.Name = name;
    }

    public void UpdateDescription(CategoryDescription description)
    {
        if (this.Description == description)
        {
            return; 
        }

        this.Description = description;
    }
}