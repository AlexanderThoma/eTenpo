using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions.Base;

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

    public void UpdateName(Name newName)
    {
        // todo: trigger domain event for updated name
        
        this.Name = newName;
    }
    
    public void UpdateDescription(Description newDescription)
    {
        this.Description = newDescription;
    }
    
    public void UpdatePrice(Price newPrice)
    {
        // todo: trigger domain event for updated price
        
        this.Price = newPrice;
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
            throw new ProductDomainException("Amount must not be less or equal than zero");
        }
        
        this.AvailableStock = this.AvailableStock.Add(amount);
    }
    
    public void RemoveStock(int amount)
    {
        if (this.AvailableStock.Value - amount < 0)
        {
            // TODO: do something useful if stock is not sufficient
            throw new ProductDomainException("Not enough stock available");
        }
        
        this.AvailableStock = this.AvailableStock.Remove(amount);
    }
}