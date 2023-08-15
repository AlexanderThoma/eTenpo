using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.DomainEvents;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Product : AggregateRoot, IAuditable
{
    // used by EF core migration
#pragma warning disable CS8618
    private Product(){}
#pragma warning restore CS8618
    
    public Product(string name, decimal price, string description, Guid categoryId)
    {
        this.ProductName = new ProductName(name);
        this.Price = new Price(price);
        this.ProductDescription = new ProductDescription(description);
        this.AvailableStock = new Stock();
        this.CategoryId = categoryId;
        this.Category = null;
        
        this.AddDomainEvent(new ProductCreatedEvent(this));
    }

    public ProductName ProductName { get; private set; }
    public Price Price { get; private set; }
    public ProductDescription ProductDescription { get; private set; }
    public Stock AvailableStock { get; private set; }
    public Guid CategoryId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    
    // used as navigation property
    public Category? Category { get; }

    public void Delete()
    {
        this.AddDomainEvent(new ProductDeletedEvent(this.Id));
    }
    
    public void UpdateName(ProductName newProductName)
    {
        if (this.ProductName == newProductName)
        {
            return;
        }
        
        this.ProductName = newProductName;
    }
    
    public void UpdateDescription(ProductDescription newProductDescription)
    {
        if (this.ProductDescription == newProductDescription)
        {
            return;
        }
        
        this.ProductDescription = newProductDescription;
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
    
    public void ChangeCategory(Guid id)
    {
        if (this.CategoryId == id)
        {
            return;
        }
        
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