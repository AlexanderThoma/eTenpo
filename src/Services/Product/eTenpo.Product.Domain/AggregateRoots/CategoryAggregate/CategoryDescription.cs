using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

public class CategoryDescription : ValueObject
{
    public const int MaxLength = 200;
    
    public CategoryDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new CategoryValidationException("Description is null or whitespace",
                new ArgumentException(null, nameof(value)));
        }
        
        if (value.Length > MaxLength)
        {
            throw new CategoryValidationException($"Description length must not exceed {MaxLength} characters");
        }
        
        this.Value = value;
    }
    
    public string Value { get; init; }
    
    public static implicit operator string(CategoryDescription categoryName)
    {
        return categoryName.Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value.ToLower();
    }
}