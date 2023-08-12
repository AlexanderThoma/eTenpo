using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
using FluentValidation;

namespace eTenpo.Product.Application.ProductFeature.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithName("name_required")
            .WithMessage("Product name cannot be empty");
        
        RuleFor(p => p.Name)
            .MaximumLength(ProductName.MaxLength)
            .WithName("name_too_long")
            .WithMessage("Product name is too long");
        
        RuleFor(p => p.Price)
            .GreaterThan(Price.MinValue)
            .WithName("negative_price")
            .WithMessage("Product price must be greater than zero");
        
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithName("description_required")
            .WithMessage("Product description cannot be empty");
        
        RuleFor(p => p.Description)
            .MaximumLength(ProductDescription.MaxLength)
            .WithName("description_too_long")
            .WithMessage("Product description is too long");

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithName("empty_categoryId")
            .WithMessage("Product category id must be set");
    }
}