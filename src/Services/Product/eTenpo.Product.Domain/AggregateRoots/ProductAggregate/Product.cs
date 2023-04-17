using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.DomainEvents;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Product : AggregateRoot
{
    public Product(Name name, Price price, Description description, CategoryId categoryId)
    {
        this.Name = name;
        this.Price = price;
        this.Description = description;
        this.AvailableStock = new Stock();
        this.CategoryId = categoryId;
    }

    public Name Name { get; private set; }
    public Price Price { get; private set; }
    public Description Description { get; private set; }
    public Stock AvailableStock { get; private set; }
    public CategoryId CategoryId { get; private set; }
    
    // used as navigation property
    public Category Category { get; }

    public void UpdateName(Name newName)
    {
        this.Name = newName;
    }
    
    public void UpdateDescription(Description newDescription)
    {
        this.Description = newDescription;
    }
    
    public void UpdatePrice(Price newPrice)
    {
        this.Price = newPrice;
        
        this.AddDomainEvent(new ProductPriceUpdatedEvent(this.Id, this.Price.Value, newPrice.Value));
    }

    // maybe skip for less complexity (event must be triggered if filtering by category
    // is done in outside service (eventual consistency must be considered)
    public void ChangeCategory(CategoryId id)
    {
        // TODO: check for valid category
        // if ()
        // {
        //     throw new ProductDomainException($"This product is already in category \"{id}\"");
        // }

        this.CategoryId = id;
    }

    public void AddStock(int amount)
    {
        if (amount < 1)
        {
            throw new ProductValidationException("Amount must not be less or equal than zero");
        }
        
        this.AvailableStock = this.AvailableStock.Add(amount);
    }
    
    public void RemoveStock(int amount)
    {
        if (this.AvailableStock.Value - amount < 0)
        {
            throw new ProductValidationException("Not enough stock available");
        }
        
        this.AvailableStock = this.AvailableStock.Remove(amount);
    }
}