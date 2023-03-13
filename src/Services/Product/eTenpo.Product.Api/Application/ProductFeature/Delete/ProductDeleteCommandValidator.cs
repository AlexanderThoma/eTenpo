
using eTenpo.Product.Api.Application.ProductFeature.Commands.Delete;
using FluentValidation;

namespace eTenpo.Product.Api.Application.ProductFeature.Delete;

public class ProductDeleteCommandValidator : AbstractValidator<ProductDeleteCommand>
{
    public ProductDeleteCommandValidator()
    {
        //RuleFor()
    }
}