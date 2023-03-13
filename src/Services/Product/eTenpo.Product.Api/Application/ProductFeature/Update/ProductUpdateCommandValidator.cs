using eTenpo.Product.Api.Application.ProductFeature.Commands.Update;
using FluentValidation;

namespace eTenpo.Product.Api.Application.ProductFeature.Update;

public class ProductUpdateCommandValidator : AbstractValidator<ProductUpdateCommand>
{
    public ProductUpdateCommandValidator()
    {
        //RuleFor()
    }
}