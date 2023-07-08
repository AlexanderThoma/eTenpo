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
        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithName("negative_price")
            .WithMessage("Product price must be greater than zero");
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithName("description_required")
            .WithMessage("Product description cannot be empty");
        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithName("empty_categoryId")
            .WithMessage("Product category id must be set");
    }
}