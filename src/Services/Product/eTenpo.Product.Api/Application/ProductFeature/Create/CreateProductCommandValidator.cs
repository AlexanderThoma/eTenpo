using FluentValidation;

namespace eTenpo.Product.Api.Application.ProductFeature.Create;

public class CreateProductCommandValidator : AbstractValidator<ProductCreateCommand>
{
    public CreateProductCommandValidator()
    {
        //RuleFor()
    }
}