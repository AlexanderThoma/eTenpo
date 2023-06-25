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
        
        this.AddDomainEvent(new ProductCreatedEvent(this));
    }

    public Name Name { get; private set; }
    public Price Price { get; private set; }
    public Description Description { get; private set; }
    public Stock AvailableStock { get; private set; }
    public CategoryId CategoryId { get; private set; }
    
    // used as navigation property
    public Category Category { get; }

    public void Delete()
    {
        this.AddDomainEvent(new ProductDeletedEvent(this.Id));
    }
    
    public void UpdateName(Name newName)
    {
        if (this.Name == newName)
        {
            return;
        }
        
        this.Name = newName;
    }
    
    public void UpdateDescription(Description newDescription)
    {
        if (this.Description == newDescription)
        {
            return;
        }
        
        this.Description = newDescription;
    }
    
    public void UpdatePrice(Price newPrice)
    {
        if (this.Price == newPrice)
        {
            return;
        }
        
        this.Price = newPrice;
        this.AddDomainEvent(new ProductPriceUpdatedEvent(this.Id, this.Price.Value, newPrice.Value));
    }
    
    public void ChangeCategory(CategoryId id)
    {
        this.CategoryId = id;
    }

    public void AddStock(int amount)
    {
        if (amount < 1)
        {
            throw new ProductValidationException("Amount must not be less or equal than zero");
        }

        var oldStock = this.AvailableStock.Value;
        this.AvailableStock = this.AvailableStock.Add(amount);
        
        this.AddDomainEvent(new StockAddedEvent(this.Id, oldStock, this.AvailableStock.Value));
    }
    
    public void RemoveStock(int amount)
    {
        if (this.AvailableStock.Value - amount < 0)
        {
            throw new ProductValidationException("Not enough stock available");
        }
        
        var oldStock = this.AvailableStock.Value;
        this.AvailableStock = this.AvailableStock.Remove(amount);
        
        this.AddDomainEvent(new StockAddedEvent(this.Id, oldStock, this.AvailableStock.Value));
    }
}