using FluentValidation;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        //RuleFor()
    }
}