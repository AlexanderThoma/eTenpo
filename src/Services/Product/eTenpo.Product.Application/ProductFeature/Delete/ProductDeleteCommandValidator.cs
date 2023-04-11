using FluentValidation;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public class ProductDeleteCommandValidator : AbstractValidator<ProductDeleteCommand>
{
    public ProductDeleteCommandValidator()
    {
        //RuleFor()
    }
}