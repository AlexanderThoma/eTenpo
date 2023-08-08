using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

public class Category : AggregateRoot, IAuditable
{
    private List<ProductAggregate.Product> products = new ();

    public Category(CategoryName name, CategoryDescription description)
    {
        this.Name = name;
        this.Description = description;
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