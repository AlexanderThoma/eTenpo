using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
using FluentValidation;

namespace eTenpo.Product.Application.ProductFeature.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithName("name_required")
            .WithMessage("Product name cannot be empty");
        
        RuleFor(p => p.Name)
            .MaximumLength(Name.MaxLength)
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
            .MaximumLength(Description.MaxLength)
            .WithName("description_too_long")
            .WithMessage("Product description is too long");
        
        RuleFor(p => p.AvailableStock)
            .GreaterThan(Stock.MinValue)
            .WithName("negative_stock")
            .WithMessage("Product stock must be greater than zero");
        
        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithName("empty_categoryId")
            .WithMessage("Product category id must be set");
        
        //  TODO: add respository checks via DI of repo in pipeline
        /*RuleFor(c => c.Name).MustAsync(async (name, _) =>
        {
            return await productRepository.IsNameUniqueAsync(name);
        }).WithMessage("The name must be unique");*/

    }
}