using FluentValidation;

namespace eTenpo.Product.Application.ProductFeature.Update;

public class ProductUpdateCommandValidator : AbstractValidator<ProductUpdateCommand>
{
    public ProductUpdateCommandValidator()
    {
        //RuleFor()
    }
}