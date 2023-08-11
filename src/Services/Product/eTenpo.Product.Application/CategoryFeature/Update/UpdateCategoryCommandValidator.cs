using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using FluentValidation;

namespace eTenpo.Product.Application.CategoryFeature.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithName("name_required")
            .WithMessage("Category name cannot be empty");
        
        RuleFor(p => p.Name)
            .MaximumLength(CategoryName.MaxLength)
            .WithName("name_too_long")
            .WithMessage("Category name is too long");
        
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithName("description_required")
            .WithMessage("Category description cannot be empty");
        
        RuleFor(p => p.Description)
            .MaximumLength(CategoryDescription.MaxLength)
            .WithName("description_too_long")
            .WithMessage("Category description is too long");
    }
}