using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.ProductAggregate;

public class Stock : ValueObject
{
    public const int MinValue = 1;
    
    public Stock(int value = 1)
    {
        if (value < MinValue)
        {
            throw new ProductValidationException("Stock can not be zero or negative",
                new ArgumentException(null, nameof(value)));
        }
        
        this.Value = value;
    }
    
    public int Value { get; init; }
    
    public static implicit operator int(Stock stock)
    {
        return stock.Value;
    }

    public Stock Add(int amount)
    {
        return new Stock(this.Value + amount);
    }
    
    public Stock Remove(int amount)
    {
        return new Stock(this.Value - amount);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}