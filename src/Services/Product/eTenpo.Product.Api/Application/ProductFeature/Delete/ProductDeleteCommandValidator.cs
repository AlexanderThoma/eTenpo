using FluentValidation;

namespace eTenpo.Product.Api.Application.ProductFeature.Delete;

public class ProductDeleteCommandValidator : AbstractValidator<ProductDeleteCommand>
{
    public ProductDeleteCommandValidator()
    {
        //RuleFor()
    }
}